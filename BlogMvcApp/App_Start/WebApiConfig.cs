using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using static BlogMvcApp.Controllers.CategoryapiController;

namespace BlogMvcApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API yapılandırması ve hizmetleri

            // Web API yolları
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        
        }

      
    }
}
