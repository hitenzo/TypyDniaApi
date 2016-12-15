using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Http;
using Microsoft.Practices.Unity;

namespace TypyDniaApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            //UnityWebActivator.Start(); to jest niepotrzene bo juz z defaulta sie inicjuje w klasie UnityMvcAtivator
            UnityConfig.RegisterComponents();

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
