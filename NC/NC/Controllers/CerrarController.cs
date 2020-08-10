using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NC.Controllers
{
    public class CerrarController : Controller
    {
        public ActionResult Logoff()
        {
            Session["CorreoUsuario"] = null;
            return RedirectToAction("Login", "Acceso");
        }
    }
}