using ContactMgmt_REST.Models;
using ContactMgmt_REST.Repositories;
using ContactMgmt_REST.Unity_DI;
using System.Net.Http.Headers;
using System.Web.Http;
using Unity;

namespace ContactMgmt_REST
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IContactRepository, ContactRepository>();
            container.RegisterType<CmsDbContext, CmsDbContext>();
            config.DependencyResolver = new UnityResolver(container);
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
