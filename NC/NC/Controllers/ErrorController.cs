using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NC.Controllers
{
    // Clase para la obtención de errores
    public class ErrorController : Controller
    {
        // Estos métodos son los que serán mostrados una vez el usuario quiera acceder a una función la cual no tenga acceso
        [HttpGet]
        public ActionResult UnauthorizedOperation(String operacion, String modulo, String msjeErrorExcepcion)
        {
            // Le mostrará la operación que está queriendo llevar a cabo
            ViewBag.operacion = operacion;
            // De qué módulo lo está queriendo ejecutar
            ViewBag.modulo = modulo;
            // Y un mensaje de error ya designado, el cual fue escrito con anterioridad
            ViewBag.msjeErrorExcepcion = msjeErrorExcepcion;
            return View();
        }
    }
}