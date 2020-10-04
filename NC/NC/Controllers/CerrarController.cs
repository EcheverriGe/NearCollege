using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NC.Controllers
{
    // Clase para cerrar sesión
    public class CerrarController : Controller
    {
        // Se está creando el método
        public ActionResult Logoff()
        {
            // Aquí se está designando la clase de "CorreoUsuario" como nula
            Session["CorreoUsuario"] = null;
            // Aquí retorna al login automáticamente
            return RedirectToAction("Login", "Acceso");
        }
    }
}