using System.Net;
using System.Web;
using System.Web.Http;

namespace Elmah.Io.Examples.WebApi.Controllers
{
    public class HelloController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            throw new HttpException(500, "Error");
        }
    }
}