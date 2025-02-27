using Projob;
using Projob.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob1.Classes
{
    [Serializable]
    public class PassengerPlane : Base, IReportable
    {
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public UInt16 FirstClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }
        public UInt16 EconomyClassSize { get; set; }

        public PassengerPlane(string[] values)
        {
            Type = values[0].Trim();
            ID = UInt64.Parse(values[1].Trim());
            Serial = values[2].Trim();
            Country = values[3].Trim();
            Model = values[4].Trim();
            FirstClassSize = UInt16.Parse(values[5].Trim());
            BusinessClassSize = UInt16.Parse(values[6].Trim());
            EconomyClassSize = UInt16.Parse(values[7].Trim());
        }

        public PassengerPlane(byte[] data)
        {
            if (data.Length < 36)
            {
                throw new ArgumentException("Invalid data array length.");
            }

            Type = Encoding.ASCII.GetString(data, 0, 3);
            ID = BitConverter.ToUInt64(data, 7);
            Serial = Encoding.ASCII.GetString(data, 15, 10);
            Country = Encoding.ASCII.GetString(data, 25, 3);
            ushort modelLength = BitConverter.ToUInt16(data, 28);
            Model = Encoding.ASCII.GetString(data, 30, modelLength);
            FirstClassSize = BitConverter.ToUInt16(data, 30 + modelLength);
            BusinessClassSize = BitConverter.ToUInt16(data, 32 + modelLength);
            EconomyClassSize = BitConverter.ToUInt16(data, 34 + modelLength);
        }

        public string Accept(Visitor visitor)
        {
            return visitor.VisitPassengerPlane(this);
        }
    }
}
