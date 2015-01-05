using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.Model
{
    public class UTMCoordinate
    {
        public int Easting, Northing;
        public int Zone;
        public char Band;

        public UTMCoordinate(int zone, char band, int easting, int northing)
        {
            Easting = easting;
            Northing = northing;
            Zone = zone;
            Band = band;
        }
    }
}
