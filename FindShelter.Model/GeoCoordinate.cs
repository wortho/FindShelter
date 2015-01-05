using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.Model
{
    public class GeoCoordinate
    {
        public double Latitude, Longitude;

        public GeoCoordinate(double latitude, double longitude)
        {
            if (latitude < -80 || latitude > 84)
                throw new ArgumentOutOfRangeException("latitude");
            Latitude = latitude;
            if (longitude < -180 || longitude > 180)
                throw new ArgumentOutOfRangeException("longitude");
            Longitude = longitude;
        }

    }
}
