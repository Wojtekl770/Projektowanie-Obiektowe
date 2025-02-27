using Projob.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Projob1.Classes
{
    [Serializable]
    [JsonDerivedType(typeof(Airport), "Airport")]
    [JsonDerivedType(typeof(Cargo), "Cargo")]
    [JsonDerivedType(typeof(CargoPlane), "CargoPlane")]
    [JsonDerivedType(typeof(Crew), "Crew")]
    [JsonDerivedType(typeof(Flight), "Flight")]
    [JsonDerivedType(typeof(Passenger), "Passenger")]
    [JsonDerivedType(typeof(PassengerPlane), "PassengerPlane")]
    public class Base
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }
    }
}
