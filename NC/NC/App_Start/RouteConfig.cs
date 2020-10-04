using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NC
{
    // En esta clase en general es donde se designa dónde abrirá o en qué pestaña se ejecutará el proyecto una vez se inicie 
    // Pero gracias a nuestro filtro, sin importar dónde inicie, será redireccionado a nuestro inicio de sesión
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
