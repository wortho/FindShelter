using System.Web.Http;

namespace FindShelter.Web.Controllers
{
    public class ValuesController : ApiController
    {
        public IHttpActionResult GetValues()
        {
            return Ok(new[] { "a", "b", "c" });
        }
    }
}
