using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace APIChrysallis
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //soluciona  problemas de búsqueda circular (cuando se buscan campos dentro de otro campo y viceversa)
            //ejemplo un ciclo tiene modulos pero los modulos tambien tienen ciclos
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            json.SerializerSettings.PreserveReferencesHandling=
            Newtonsoft.Json.PreserveReferencesHandling.None;
            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
