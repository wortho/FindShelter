using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.UdINaturenService
{
    public partial class SearchResultItem
    {

        public string Name { get; set; }
        public int FacilityID { get; set; }
        public string LongDescription { get; set; }

        public int Distance2OriginalItem { get; set; }

        public string IconPositionGeometryWKT { get; set; }

        public int MainImageID { get; set; }

        public int RouteLength { get; set; }

        public string ShortDescription { get; set; }

        public int SubCategoryID { get; set; }

        public string URL { get; set; } 


        public static int[] ParseWKTPoint(string wktPoint)
        {
            int[] points = new int[2]{0,0};
            if (wktPoint.Contains("POINT"))
            {
                string[] delimeters = new string[] { "POINT(", ")", " " };
                string[] strings = wktPoint.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strings.Count(); i++)
                {
                    int.TryParse(strings[i], out points[i]);
                }
            }
            return points;
        }
    }
}
