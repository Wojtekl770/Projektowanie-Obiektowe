using Projob.Media;
using Projob1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob
{
    public class Television: Medias
    {
        public string Name { get; set; }

        public Television(string name)
        {
            Name = name;
        }

        public override string VisitAirport(Airport airport)
        {
            string s = $"<An image of {airport.Name} airport>";
            return s;
        }

        public override string VisitCargoPlane(CargoPlane cargoPlane)
        {
            string s = $"<An image of {cargoPlane.Serial} cargo plane>";
            return s;
        }

        public override string VisitPassengerPlane(PassengerPlane passengerPlane)
        {
            string s = $"<An image of {passengerPlane.Serial} passenger plane>";
            return s;
        }
    }
}
