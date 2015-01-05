using FindShelter.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FindShelter.Web.Controllers
{

    public class FacilitiesController : ApiController
    {
        private IFacilityService FacilityService;

        public FacilitiesController()
        {
            FacilityService = new FindShelter.UdINaturenService.FacilityService();
        }

        public FacilitiesController(IFacilityService service)
        {
            FacilityService = service;
        }
       
        // GET: api/Facilities
        public async Task<IHttpActionResult> GetFacilities()
        {
            SetNoCacheHeader();
            var facilities = await FindFacilities();

            return Ok<IEnumerable<Facility>>(facilities);
        }


        protected static void SetNoCacheHeader()
        {
            if (HttpContext.Current != null && HttpContext.Current.Response != null)
            {
                HttpContext.Current.Response.CacheControl = "No-Cache";
            }
        }

        private async Task<IEnumerable<Facility>> FindFacilities()
        {
            var facilities = await FacilityService.GetFacilities();
            return facilities;
        }

        // GET: api/Facilities/1
        public async Task<IHttpActionResult> GetFacility(int id)
        {
            SetNoCacheHeader();

            var facility = await FindFacility(id);

            return Ok<Facility>(facility);
        }
        private async Task<Facility> FindFacility(int id)
        {
            var facility = await FacilityService.GetFacility(id);
            return facility;
        }
    }
}
