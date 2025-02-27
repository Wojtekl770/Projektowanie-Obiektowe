using Projob1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob.Media
{
    public interface Visitor
    {
        public string VisitAirport(Airport airport);
        public string VisitCargoPlane(CargoPlane cargoPlane);
        public string VisitPassengerPlane(PassengerPlane passengerPlane);
    }
}
