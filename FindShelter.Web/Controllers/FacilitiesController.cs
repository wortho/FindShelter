using FindShelter.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FindShelter.Web.Controllers
{

    public class FacilitiesController : ApiController
    {
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

        private async Task<Facility[]> FindFacilities()
        {
            var facilities = new[] { 
                new Facility(1, "Name1", "Description1", new GeoCoordinate(1, 0)) ,
                new Facility(2, "Name2", "Description2", new GeoCoordinate(2, 0)) ,
                new Facility(3, "Name3", "Description3", new GeoCoordinate(2, 0))
            };
            await Task.Delay(100);
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
            var facility = new Facility(id, "Name", "Description", new GeoCoordinate(0, 0));
            await Task.Delay(100);
            return facility;
        }
    }
}
