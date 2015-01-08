using FindShelter.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FindShelter.Web.Controllers
{

    public class FacilitiesController : ApiController
    {
        public IFacilityService FacilityService { get; private set; }

        public FacilitiesController()
        {
            FacilityService = new FindShelter.UdINaturenService.FacilityService();
        }

        public FacilitiesController(IFacilityService service)
        {
            FacilityService = service;
        }

        // GET: api/Facilities?box=
        public async Task<IHttpActionResult> GetFacilities(double latitudeSW, double longitudeSW, double latitudeNE, double longitudeNE)
        {
            try
            {
                var box = new BoundingBox(new GeoCoordinate(latitudeSW, longitudeSW), new GeoCoordinate(latitudeNE, longitudeNE));
                SetNoCacheHeader();
                var facilities = await FindFacilities(box);

                if (facilities != null)
                {
                    return Ok<IEnumerable<Facility>>(facilities);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }

            return NotFound();
        }


        protected static void SetNoCacheHeader()
        {
            if (HttpContext.Current != null && HttpContext.Current.Response != null)
            {
                HttpContext.Current.Response.CacheControl = "No-Cache";
            }
        }

        private async Task<IEnumerable<Facility>> FindFacilities(BoundingBox box)
        {
            var facilities = await FacilityService.GetFacilities(box);
            return facilities;
        }

        // GET: api/Facilities/1
        public async Task<IHttpActionResult> GetFacility(int id)
        {
            SetNoCacheHeader();

            var facility = await FindFacility(id);

            if (facility != null)
            {
                return Ok<Facility>(facility);
            }

            return NotFound();

        }
        private async Task<Facility> FindFacility(int id)
        {
            var facility = await FacilityService.GetFacility(id);
            return facility;
        }
    }
}
