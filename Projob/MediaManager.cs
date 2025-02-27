using System;
using System.Collections.Generic;
using Projob.Media;
using NetworkSourceSimulator;
using Projob1.Classes;
using Projob;

namespace Projob1
{
    public class MediaManager
    {
        private List<IReportable> createdObjects = new List<IReportable>();
        private List<Medias> mediaList = new List<Medias>();

        public void Run(string filePath)
        {
            InitializeMedia();

            NetworkSourceSimulator.NetworkSourceSimulator networkSource = new NetworkSourceSimulator.NetworkSourceSimulator(filePath, 1000, 2000);

            Thread networkThread = new Thread(new ThreadStart(networkSource.Run));
            networkThread.Start();

            networkSource.OnNewDataReady += HandleNewData;

            bool appRunning = true;
            while (appRunning)
            {
                string input = Console.ReadLine();
                switch (input)
                {
                    case "report":
                        GenerateAndDisplayNews();
                        break;
                    case "exit":
                        appRunning = false;
                        break;
                    default:
                        Console.WriteLine("Nieznane polecenie.");
                        break;
                }
            }
            networkThread.Join();

            Console.WriteLine("Aplikacja zakończona.");
        }

        private void InitializeMedia()
        {
            // Dodaj telewizje
            mediaList.Add(new Television("Telewizja Abelowa"));
            mediaList.Add(new Television("Kanał TV-tensor"));

            // Dodaj radia
            mediaList.Add(new Radio("Radio Kwantyfikator"));
            mediaList.Add(new Radio("Radio Shmem"));

            // Dodaj gazety
            mediaList.Add(new Newspaper("Gazeta Kategoryczna"));
            mediaList.Add(new Newspaper("Dziennik Politechniczny"));
        }

        private void GenerateAndDisplayNews()
        {
            List<IReportable> reportableObjectsCopy = new List<IReportable>(createdObjects);
            NewsGenerator newsGenerator = new NewsGenerator(reportableObjectsCopy, mediaList);

            foreach (var news in newsGenerator.GenerateNews())
            {
                Console.WriteLine(news);
            }
        }


        private void HandleNewData(object sender, NewDataReadyArgs e)
        {
            if (sender is NetworkSourceSimulator.NetworkSourceSimulator source)
            {
                Message message = source.GetMessageAt(e.MessageIndex);
                string type = System.Text.Encoding.ASCII.GetString(message.MessageBytes, 0, 3);
                if (type == "NCP")
                {
                    CargoPlane cp = new CargoPlane(message.MessageBytes);
                    createdObjects.Add(cp);
                }
                if (type == "NAI")
                {
                    Airport air = new Airport(message.MessageBytes);
                    createdObjects.Add(air);
                }
                if (type == "NPP")
                {
                    PassengerPlane pp = new PassengerPlane(message.MessageBytes);
                    createdObjects.Add(pp);
                }
            }
        }
    }
}
