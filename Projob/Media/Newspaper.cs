using Projob.Media;
using Projob1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob
{
    public class Newspaper : Medias
    {
        public string Name { get; set; }

        public Newspaper(string name)
        {
            Name = name;
        }

        public override string VisitAirport(Airport airport)
        {
            string s = $"{Name} - A report from the {airport.Name} airport, {airport.Country}.";
            return s;
        }

        public override string VisitCargoPlane(CargoPlane cargoPlane)
        {
            string s = $"{Name} - An interview with the crew of {cargoPlane.Serial}.";
            return s;
        }

        public override string VisitPassengerPlane(PassengerPlane passengerPlane)
        {
            string s = $"{Name} - Breaking news! {passengerPlane.Model} aircraft loses EASA fails certification after inspection of {passengerPlane.Serial}.";
            return s;
        }
    }
}
