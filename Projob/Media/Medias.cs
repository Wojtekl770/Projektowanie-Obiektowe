using Projob1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob.Media
{
    public abstract class Medias: Visitor
    {
        public string Name { get; set; }

        public abstract string VisitAirport(Airport airport);
        public abstract string VisitCargoPlane(CargoPlane cargoPlane);
        public  abstract string VisitPassengerPlane(PassengerPlane passengerPlane);
    }
}
