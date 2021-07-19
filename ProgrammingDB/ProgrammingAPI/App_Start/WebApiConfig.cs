using ProgrammingAPI.Attributes;
using ProgrammingAPI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ProgrammingAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new ApiExceptionAttribute());//Project level try catch blog
            config.MessageHandlers.Add(new ApiKeyHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
