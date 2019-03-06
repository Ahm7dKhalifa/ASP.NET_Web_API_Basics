using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace app1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //for responce message in json only
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            //for responce message in xml only
            //config.Formatters.Remove(config.Formatters.JsonFormatter);
        }
    }
}
