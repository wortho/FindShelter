using FindShelter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.UdINaturenService
{
    public class FacilityService : IFacilityService
    {
        public async Task<IEnumerable<Model.Facility>> GetFacilities()
        {
            List<Model.Facility> facilities = new List<Model.Facility>();
            ServiceClient svc = new ServiceClient();
            SearchResult findResult = await svc.FindFacilities(1, "720175, 6213286, 710175, 6113286");
            List<SearchResultItem> items = await svc.GetSearchResultItems(findResult.SearchResultID);
            foreach (var item in items)
            {
                facilities.Add(new Model.Facility(item.FacilityID, item.Name, item.ShortDescription, item.LongDescription, new GeoCoordinate(0, 0)));
            }
            return facilities;
        }

        public async Task<Model.Facility> GetFacility(int id)
        {
            ServiceClient svc = new ServiceClient();
            Facility facility = await svc.GetFacility(1, id);
            return new Model.Facility(facility.FacilityID, facility.Name, facility.ShortDescription, facility.LongDescription, new GeoCoordinate(0, 0));    
        }
    }
}
