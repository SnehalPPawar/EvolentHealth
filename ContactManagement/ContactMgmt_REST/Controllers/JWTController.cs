using ContactMgmt_REST.Authentication;
using System.Web.Http;

namespace ContactMgmt_REST.Controllers
{
    public class JWTController : ApiController
    {
        // GET: 
        public IHttpActionResult GetToken(string username)
        {
            return Ok(TokenManager.GenerateToken(username));
        }
    }
}
