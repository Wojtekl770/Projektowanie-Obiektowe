using NetworkSourceSimulator;
using Projob1.Classes;
using FlightTrackerGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Projob1;
using Mapsui.Projections;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlightTrackerGUI
{
    public class FlightManager
    {
        private List<Base> createdObjects = new List<Base>();
        private List<Airport> AirportsMap { get; set; } = new List<Airport>();
        private List<Flight> flightsMap { get; set; } = new List<Flight>();

        private DateTime acceleratedTime = DateTime.Today;
        private readonly object flightsLock = new object();

        public void Run(string filePath)
        {
            NetworkSourceSimulator.NetworkSourceSimulator networkSource = new NetworkSourceSimulator.NetworkSourceSimulator(filePath, 1, 2);

            Thread networkThread = new Thread(new ThreadStart(networkSource.Run));
            networkThread.Start();

            networkSource.OnNewDataReady += HandleNewData;
            Thread.Sleep(5000);

            Thread updateThread = new Thread(UpdateDataLoop);
            updateThread.Start();
            Runner.Run();
            networkThread.Join();

        }

        private void HandleNewData(object sender, NewDataReadyArgs e)
        {
            if (sender is NetworkSourceSimulator.NetworkSourceSimulator source)
            {
                Message message = source.GetMessageAt(e.MessageIndex);

                Base obj = Factory.CreateObjectBit(message.MessageBytes);
                createdObjects.Add(obj);

                string type = System.Text.Encoding.ASCII.GetString(message.MessageBytes, 0, 3);

                lock (flightsLock)
                {
                    if (obj.Type == "NFL")
                    {
                        Flight fl = new Flight(message.MessageBytes);
                        flightsMap.Add(fl);
                    }
                    if (obj.Type == "NAI")
                    {
                        Airport air = new Airport(message.MessageBytes);
                        AirportsMap.Add(air);
                    }
                }
            }
        }

        private void UpdateDataLoop()
        {
            while (true)
            {
                RemoveLandedFlights();
                acceleratedTime = acceleratedTime.AddSeconds(5);
                FlightsGUIData flightsGUIData = ConvertToGUIData(flightsMap);
                Runner.UpdateGUI(flightsGUIData);
                Thread.Sleep(5);
            }
        }


        public Airport GetAirportByID(UInt64 airportID)
        {
            return AirportsMap.FirstOrDefault(airport => airport.ID == airportID);
        }

        private FlightsGUIData ConvertToGUIData(List<Flight> flights)
        {
            List<FlightGUI> flightGUIs = new List<FlightGUI>();
            lock (flightsLock)
            {
                foreach (var flight in flights)
                {
                    UpdateFlightPosition(flight);
                    if (flight.Longitude.HasValue && flight.Latitude.HasValue)
                    {
                        FlightGUI flightGUI = GetFlightGUI(flight);
                        flightGUIs.Add(flightGUI);
                    }
                }
            }
            return new FlightsGUIData(flightGUIs);
        }

        public FlightGUI GetFlightGUI(Flight flight)
        {
            WorldPosition worPos = new WorldPosition();
            worPos.Longitude = (float)flight.Longitude;
            worPos.Latitude = (float)flight.Latitude;
            FlightGUI flightGUI = new FlightGUI
            {
                ID = flight.ID,
                WorldPosition = worPos,
                MapCoordRotation = CalculateMapCoordRotation(flight)
            };
            return flightGUI;
        }

        private void UpdateFlightPosition(Flight flight)
        {
            Airport originAirport = GetAirportByID(flight.OriginID);
            Airport targetAirport = GetAirportByID(flight.TargetID);

            DateTime takeoffTime = DateTime.Parse(flight.TakeoffTime);
            DateTime landingTime = DateTime.Parse(flight.LandingTime);

            if (takeoffTime > acceleratedTime)
            {
                flight.SetLongitude(null);
                flight.SetLatitude(null);
                return;
            }

            if (!flight.Longitude.HasValue || !flight.Latitude.HasValue)
            {
                flight.SetLongitude(originAirport.Longitude);
                flight.SetLatitude(originAirport.Latitude);
            }
            else
            {
                TimeSpan flightDuration = landingTime - takeoffTime;
                TimeSpan elapsedTime = acceleratedTime - takeoffTime;

                float ratio = (float)(elapsedTime.TotalMilliseconds / flightDuration.TotalMilliseconds);
                float interpolatedLongitude = originAirport.Longitude + (targetAirport.Longitude - originAirport.Longitude) * ratio;
                float interpolatedLatitude = originAirport.Latitude + (targetAirport.Latitude - originAirport.Latitude) * ratio;

                flight.SetLongitude(interpolatedLongitude);
                flight.SetLatitude(interpolatedLatitude);
            }
        }


        private double CalculateMapCoordRotation(Flight flight)
        {
            Airport targetAirport = GetAirportByID(flight.TargetID);
            var startPoint = SphericalMercator.FromLonLat((double)flight.Longitude, (double)flight.Latitude);
            var endPoint = SphericalMercator.FromLonLat((double)targetAirport.Longitude, (double)targetAirport.Latitude);

            float deltaX = (float)(endPoint.x - startPoint.x);
            float deltaY = (float)(endPoint.y - startPoint.y);

            double angle = Math.Atan2(deltaX, deltaY);

            return angle;
        }

        private void RemoveLandedFlights()
        {
            List<Flight> landedFlights = flightsMap.Where(flight => DateTime.Parse(flight.LandingTime) < acceleratedTime).ToList();
            foreach (var flight in landedFlights)
            {
                flightsMap.Remove(flight);
            }
        }
    }
}