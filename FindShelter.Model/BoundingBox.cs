using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.Model
{
    public class BoundingBox
    {
        public GeoCoordinate SW { get; set; }
        public GeoCoordinate NE { get; set; }

        public BoundingBox(GeoCoordinate sw, GeoCoordinate ne)
        {
            SW = sw;
            NE = ne;
        }
    }
}
