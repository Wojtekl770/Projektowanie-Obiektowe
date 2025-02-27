using Projob.Media;
using Projob1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob
{
    public class Radio : Medias
    {
        public string Name { get; set; }

        public Radio(string name)
        {
            Name = name;
        }

        public override string VisitAirport(Airport airport)
        {
            string s = $"Reporting for {Name}, Ladies and gentelmen, we are at the {airport.Name} airport.";
            return s;
        }

        public override string VisitCargoPlane(CargoPlane cargoPlane)
        {
            string s = $"Reporting for {Name}, Ladies and gentelmen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";
            return s;
        }

        public override string VisitPassengerPlane(PassengerPlane passengerPlane)
        {
            string s = $"Reporting for {Name}, Ladies and gentelmen, we’ve just witnessed {passengerPlane.Serial} take off.";
            return s;
        }
    }
}
