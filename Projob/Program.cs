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
using FlightTrackerGUI;


namespace Projob1
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "example_data.ftr";

            MediaManager communication = new MediaManager();
            communication.Run(filePath);
        }
    }
}