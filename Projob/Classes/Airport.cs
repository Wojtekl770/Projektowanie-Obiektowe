using Projob;
using Projob.Media;
using System;
using System.Globalization;
using System.Text;

namespace Projob1.Classes
{
    [Serializable]
    public class Airport : Base, IReportable
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public string Country { get; set; }

        public Airport(string[] values)
        {
            Type = values[0].Trim();
            ID = UInt64.Parse(values[1].Trim());
            Name = values[2].Trim();
            Code = values[3].Trim();
            Longitude = float.Parse(values[4].Trim(), CultureInfo.InvariantCulture.NumberFormat);
            Latitude = float.Parse(values[5].Trim(), CultureInfo.InvariantCulture.NumberFormat);
            AMSL = float.Parse(values[6].Trim(), CultureInfo.InvariantCulture.NumberFormat);
            Country = values[7].Trim();
        }

        public Airport(byte[] data)
        {
            if (data.Length < 35)
            {
                throw new ArgumentException("Invalid data array length.");
            }

            Type = Encoding.ASCII.GetString(data, 0, 3);
            ID = BitConverter.ToUInt64(data, 7);
            ushort nameLength = BitConverter.ToUInt16(data, 15);
            Name = Encoding.ASCII.GetString(data, 17, nameLength);
            Code = Encoding.ASCII.GetString(data, 17 + nameLength, 3);
            Longitude = BitConverter.ToSingle(data, 20 + nameLength);
            Latitude = BitConverter.ToSingle(data, 24 + nameLength);
            AMSL = BitConverter.ToSingle(data, 28 + nameLength);
            Country = Encoding.ASCII.GetString(data, 32 + nameLength, 3);
        }

        public string Accept(Visitor vis)
        {
            return vis.VisitAirport(this);
        }
    }
}
