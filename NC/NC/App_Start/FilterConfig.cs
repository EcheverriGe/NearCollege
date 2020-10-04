using System.Web;
using System.Web.Mvc;

namespace NC
{
    // En esta clase estamos añadiendo cierto tipo de filtros
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // En este caso, añadimos la validación para evitar el acceso al sitio web si no hay un usuario logueado o,
            // una sesión latente
            filters.Add(new Filters.VerificaSession());
        }
    }
}
