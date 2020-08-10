using NC.Controllers;
using NC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NC.Filters
{
    public class VerificaSession : ActionFilterAttribute
    {
        private Tbl_Usuarios oUsuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            var oUsuario = (Tbl_Usuarios)HttpContext.Current.Session["CorreoUsuario"];

            if(oUsuario == null)
            {
                if(filterContext.Controller is AccesoController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Acceso/Login");
                }
            }
            else
            {
                if (filterContext.Controller is AccesoController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }
            base.OnActionExecuting(filterContext);

            //try
            //{
              //  base.OnActionExecuting(filterContext);
              //
                //oUsuario = (Tbl_Usuarios)HttpContext.Current.Session["CorreoUsuario"];
                //if (oUsuario == null)
                //{
                  //  if (filterContext.Controller is AccesoController == false)
                    //{
                      //  filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                    //}
                //}
            //}
            //catch (Exception)
            //{
              //  filterContext.Result = new RedirectResult("~/Acceso/Login");
            //}
        }
    }
}