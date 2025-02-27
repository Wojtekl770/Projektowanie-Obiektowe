using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;                    
using System.Text.Json;
using Projob1.Classes;

namespace Projob1
{
    public class Serialization
    {
        public static List<Base> ReadFTRFile(string fileName)
        {
            List<Base> objects = new List<Base>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    objects.Add(Factory.CreateObject(values));
                }
            }

            return objects;
        }

        public static void WriteToJsonFile(List<Base> objects, string fileName)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(objects, options);
            File.WriteAllText(fileName, json);
        }
    }
}
