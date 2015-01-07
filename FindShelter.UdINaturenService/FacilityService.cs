using FindShelter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.UdINaturenService
{
    public class FacilityService : IFacilityService
    {
        public async Task<IEnumerable<Model.Facility>> GetFacilities(BoundingBox box)
        {
            var boxString = GetUTMBoxString(box);
            List<Model.Facility> facilities = new List<Model.Facility>();
            ServiceClient svc = new ServiceClient();
            SearchResult findResult = await svc.FindFacilities(1, boxString);
            List<SearchResultItem> items = await svc.GetSearchResultItems(findResult.SearchResultID);
            foreach (var item in items)
            {
                var point = GetFacilityLocation(item.IconPositionGeometryWKT);
                facilities.Add(new Model.Facility(item.FacilityID, item.Name, item.ShortDescription, item.LongDescription, new GeoCoordinate(0, 0)));
            }
            return facilities;
        }

        private string GetUTMBoxString(BoundingBox box)
        {
            var testString = JsonConvert.SerializeObject(box);
            var swUTM = GeoConverter.Converter.ConvertWGS84ToUtm(box.SW);
            var neUTM = GeoConverter.Converter.ConvertWGS84ToUtm(box.NE);
            return string.Format("{0},{1},{2},{3}", swUTM.Easting, swUTM.Northing, neUTM.Easting, neUTM.Northing);
        }

        public async Task<Model.Facility> GetFacility(int id)
        {
            ServiceClient svc = new ServiceClient();
            Facility facility = await svc.GetFacility(1, id);
            var point = GetFacilityLocation(facility.IconPositionGeometryWKT);
            return new Model.Facility(facility.FacilityID, facility.Name, facility.ShortDescription, facility.LongDescription, point);
        }

        private static GeoCoordinate GetFacilityLocation(string geometryWKT)
        {
            if (geometryWKT != null)
            {
                var utm = GeoConverter.Converter.ConvertWKTToUtm(geometryWKT);
                var point = GeoConverter.Converter.ConvertUtmToWGS84(utm);
                return point;
            }
            return null;
        }
    }
}
