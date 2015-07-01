using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DigitalVaccination
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            System.Web.Mvc.ControllerBuilder.Current.DefaultNamespaces.Add("DigitalVaccination.ApiController");
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
           
        }
    }
}
