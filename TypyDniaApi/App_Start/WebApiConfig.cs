using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Http;

namespace TypyDniaApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
