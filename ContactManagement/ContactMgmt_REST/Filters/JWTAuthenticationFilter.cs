using ContactMgmt_REST.Authentication;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ContactMgmt_REST.Filters
{
    public class JWTAuthenticationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            if (!IsUserAuthorized(filterContext))
            {
                Challenge(filterContext);
                return;
            }
            base.OnAuthorization(filterContext);
        }

        private bool IsUserAuthorized(HttpActionContext filterContext)
        {
            string token = filterContext.Request.Headers.Authorization?.Parameter;
            if (string.IsNullOrWhiteSpace(token))
                return false;
            return TokenManager.IsTokenValidate(token);
        }

        private void Challenge(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }   
}