using Projob;
using Projob.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob1.Classes
{
    [Serializable]
    public class CargoPlane : Base, IReportable
    {
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public float MaxLoad { get; set; }

        public CargoPlane(string[] values)
        {
            Type = values[0].Trim();
            ID = UInt64.Parse(values[1].Trim());
            Serial = values[2].Trim();
            Country = values[3].Trim();
            Model = values[4].Trim();
            MaxLoad = float.Parse(values[5].Trim(), CultureInfo.InvariantCulture.NumberFormat);
        }

        public CargoPlane(byte[] data)
        {
            if (data.Length < 34)
            {
                throw new ArgumentException("Invalid data array length.");
            }

            Type = Encoding.ASCII.GetString(data, 0, 3);
            ID = BitConverter.ToUInt64(data, 7);
            Serial = Encoding.ASCII.GetString(data, 15, 10);
            Country = Encoding.ASCII.GetString(data, 25, 3);
            ushort modelLength = BitConverter.ToUInt16(data, 28);
            Model = Encoding.ASCII.GetString(data, 30, modelLength);
            MaxLoad = BitConverter.ToSingle(data, 30 + modelLength);
        }

        public string Accept(Visitor vis)
        {
            return vis.VisitCargoPlane(this);
        }
    }
}
