using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob1.Classes
{
    [Serializable]
    public class Crew : Base
    {
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public UInt64 Practice { get; set; }
        public string Role { get; set; }

        public Crew(string[] values)
        {
            Type = values[0].Trim();
            ID = UInt64.Parse(values[1].Trim());
            Name = values[2].Trim();
            Age = UInt64.Parse(values[3].Trim());
            Phone = values[4].Trim();
            Email = values[5].Trim();
            Practice = UInt64.Parse(values[6].Trim());
            Role = values[7].Trim();
        }

        public Crew(byte[] data)
        {
            if (data.Length < 36)
            {
                throw new ArgumentException("Invalid data array length.");
            }

            Type = Encoding.ASCII.GetString(data, 0, 3);
            ID = BitConverter.ToUInt64(data, 7);
            UInt16 nameLength = BitConverter.ToUInt16(data, 15);
            Name = Encoding.ASCII.GetString(data, 17, nameLength);
            Age = BitConverter.ToUInt16(data, 17 + nameLength);
            Phone = Encoding.ASCII.GetString(data, 19 + nameLength, 12);
            UInt16 emailLength = BitConverter.ToUInt16(data, 31 + nameLength);
            Email = Encoding.ASCII.GetString(data, 33 + nameLength, emailLength);
            Practice = BitConverter.ToUInt16(data, 33 + nameLength + emailLength);
            Role = Encoding.ASCII.GetString(data, 35 + nameLength + emailLength, 1);
        }
    }
}
