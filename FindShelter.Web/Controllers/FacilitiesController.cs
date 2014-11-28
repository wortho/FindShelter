using System.Web.Http;

namespace FindShelter.Web.Controllers
{
    public class FacilitiesController : ApiController
    {
        public IHttpActionResult GetValues()
        {
            return Ok(new[] { "a", "b", "c" });
        }
    }
}
