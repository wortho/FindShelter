using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.UdINaturenService
{
    public class Facility
    {
        public string Name { get; set; }

        public int FacilityID { get; set; }

        public string LongDescription { get; set; }

        public string FacilityGeometryWKT;

        public string GeometryType;

        public string IconPositionGeometryWKT;

        public int[] ImageIDList;

        public int Length;

        public int MainImageID;

        public int OrganisationID;

        public bool RejseplanLinkTilladt;

        public string ShortDescription;

        public int SubCategoryID;

        public string Url;
    }
}
