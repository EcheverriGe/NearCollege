using NC.Controllers;
using NC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NC.Filters
{
    // Clase para la verificación de sesión
    public class VerificaSession : ActionFilterAttribute
    {
        // Este método es privado, y hace referencia a la tabla Tbl_Usuarios en general
        private Tbl_Usuarios oUsuario;

        // Este método es un filtro de acción para aplicar precauciones, almacenamiento en cache y autorizaciones
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Esta línea de código define que, oUsuario sea la sesión actual en el sitio web, entrando a la base de datos, a la tabla
            // Tbl_Usuarios definiendo como tal que, de esa tabla, el atributo CorreoUsuario
            var oUsuario = (Tbl_Usuarios)HttpContext.Current.Session["CorreoUsuario"];

            // Como tal este if, define que, si oUsuario (Que en este caso, como vemos arriba, es la sesión) es nulo o, está vacío
            // pues gracias al filtro, nos redireccione automáticamente a la ruta: /Acceso/Login sin importa qué, que en este caso
            // es el inicio de sesión
            if (oUsuario == null)
            {
                if(filterContext.Controller is AccesoController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Acceso/Login");
                }
            }

            // Y, este else, es lo contrato, cumple con la siguiente función: si oUsuario, o en este caso, la validación del filtro
            // es true, entonces nos redicreccionará de forma automática y sin problemas a la siguiente ruta: /Home/Index
            // que en este caso, es el index principal
            else
            {
                if (filterContext.Controller is AccesoController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}