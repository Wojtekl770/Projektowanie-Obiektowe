using Projob.Media;
using Projob1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob1
{
    public static class Factory
    {
        private static Dictionary<string, Func<string[], Base>> factoryDict = new Dictionary<string, Func<string[], Base>>
        {
            { "C", values => new Classes.Crew(values) },
            { "P", values => new Classes.Passenger(values) },
            { "CA", values => new Classes.Cargo(values) },
            { "CP", values => new Classes.CargoPlane(values) },
            { "PP", values => new Classes.PassengerPlane(values) },
            { "AI", values => new Classes.Airport(values) },
            { "FL", values => new Classes.Flight(values) }
        };


        public static Base CreateObject(string[] values)
        {
            string type = values[0].Trim();
            if (factoryDict.ContainsKey(type))
            {
                Func<string[], Base> factory = factoryDict[type];
                return factory(values);
            }
            else
            {
                Console.WriteLine($"Nieznana klasa: {type}");
                return null;
            }
        }

        private static Dictionary<string, Func<byte[], Base>> factoryDictBit = new Dictionary<string, Func<byte[], Base>>
        {
            { "NCR", data => new Classes.Crew(data) },
            { "NPA", data => new Classes.Passenger(data) },
            { "NCA", data => new Classes.Cargo(data) },
            { "NCP", data => new Classes.CargoPlane(data) },
            { "NPP", data => new Classes.PassengerPlane(data) },
            { "NAI", data => new Classes.Airport(data) },
            { "NFL", data => new Classes.Flight(data) }
        };


        public static Base CreateObjectBit(byte[] data)
        {
            if (data == null || data.Length < 3)
            {
                Console.WriteLine("Nieprawidłowe dane.");
                return null;
            }

            string type = System.Text.Encoding.ASCII.GetString(data, 0, 3);

            if (factoryDictBit.ContainsKey(type))
            {
                Func<byte[], Base> factory = factoryDictBit[type];
                return factory(data);
            }
            else
            {
                Console.WriteLine($"Nieznana klasa: {type}");
                return null;
            }
        }
    }
}
