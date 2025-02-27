using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob1.Classes
{
    [Serializable]
    public class Flight : Base
    {
        public UInt64 OriginID { get; set; }
        public UInt64 TargetID { get; set; }
        public string TakeoffTime { get; set; }
        public string LandingTime { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public float? AMSL { get; set; }
        public UInt64 PlaneID { get; set; }
        public UInt64[] CrewIDs { get; set; }
        public UInt64[] LoadIDs { get; set; }

        public Flight(string[] values)
        {
            Type = values[0].Trim();
            ID = UInt64.Parse(values[1].Trim());
            OriginID = UInt64.Parse(values[2].Trim());
            TargetID = UInt64.Parse(values[3].Trim());
            TakeoffTime = values[4].Trim();
            LandingTime = values[5].Trim();
            Longitude = float.Parse(values[6].Trim(), CultureInfo.InvariantCulture.NumberFormat);
            Latitude = float.Parse(values[7].Trim(), CultureInfo.InvariantCulture.NumberFormat);
            AMSL = float.Parse(values[8].Trim(), CultureInfo.InvariantCulture.NumberFormat);
            PlaneID = UInt64.Parse(values[9].Trim());
            // Removing "[" and "]" from the string
            CrewIDs = Array.ConvertAll(values[10].Substring(1, values[10].Length - 2).Split(';'), UInt64.Parse);
            LoadIDs = Array.ConvertAll(values[11].Substring(1, values[11].Length - 2).Split(';'), UInt64.Parse);
        }

        public Flight(byte[] data)
        {
            if (data.Length < 59)
            {
                throw new ArgumentException("Invalid data array length.");
            }

            Type = Encoding.ASCII.GetString(data, 0, 3);
            ID = BitConverter.ToUInt64(data, 7);
            OriginID = BitConverter.ToUInt64(data, 15);
            TargetID = BitConverter.ToUInt64(data, 23);
            TakeoffTime = DateTime.UnixEpoch.AddMilliseconds(BitConverter.ToInt64(data, 31)).ToString();
            LandingTime = DateTime.UnixEpoch.AddMilliseconds(BitConverter.ToInt64(data, 39)).ToString();
            PlaneID = BitConverter.ToUInt64(data, 47);

            ushort crewCount = BitConverter.ToUInt16(data, 55);
            CrewIDs = new UInt64[crewCount];
            for (int i = 0; i < crewCount; i++)
            {
                CrewIDs[i] = BitConverter.ToUInt64(data, 57 + i * 8);
            }

            ushort passengersCargoCountOffset = (ushort)(57 + crewCount * 8);
            ushort passengersCargoCount = BitConverter.ToUInt16(data, passengersCargoCountOffset);
            LoadIDs = new UInt64[passengersCargoCount];
            for (int i = 0; i < passengersCargoCount; i++)
            {
                LoadIDs[i] = BitConverter.ToUInt64(data, passengersCargoCountOffset + 2 + i * 8);
            }
        }

        public void SetLongitude(float? longitude)
        {
            Longitude = longitude;
        }

        public void SetLatitude(float? latitude)
        {
            Latitude = latitude;
        }
    }
}
