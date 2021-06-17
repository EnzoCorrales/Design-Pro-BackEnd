using InternalServices.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace InternalServices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            var cors = new EnableCorsAttribute("http://localhost:8080", "*", "*");
            config.EnableCors(cors);

            config.MessageHandlers.Add(new TokenHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
