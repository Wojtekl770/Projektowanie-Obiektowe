using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob1.Classes
{
    [Serializable]
    public class Cargo : Base
    {
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(string[] values)
        {
            Type = values[0].Trim();
            ID = UInt64.Parse(values[1].Trim());
            Weight = float.Parse(values[2].Trim(), CultureInfo.InvariantCulture.NumberFormat);
            Code = values[3].Trim();
            Description = values[4].Trim();
        }

        public Cargo(byte[] data)
        {
            if (data.Length < 27)
            {
                throw new ArgumentException("Invalid data array length.");
            }

            Type = Encoding.ASCII.GetString(data, 0, 3);
            ID = BitConverter.ToUInt64(data, 7);
            Weight = BitConverter.ToUInt32(data, 15);
            Code = Encoding.ASCII.GetString(data, 19, 6);
            ushort descriptionLength = BitConverter.ToUInt16(data, 25);
            Description = Encoding.ASCII.GetString(data, 27, descriptionLength);
        }
    }

}
