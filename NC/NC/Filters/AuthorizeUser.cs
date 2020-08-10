using NC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NC.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private Tbl_Usuarios oUsuario;
        private NCEntities db = new NCEntities();
        private int IdOperacion;

        public AuthorizeUser(int IdOperacion = 0)
        {
            this.IdOperacion = IdOperacion;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string NombreOperacion = "";
            string NombreModulo = "";
            try
            {
                oUsuario = (Tbl_Usuarios)HttpContext.Current.Session["CorreoUsuario"];
                var lstMisOperaciones = from m in db.Tbl_Roles_Operaciones
                                        where m.IdRol == oUsuario.IdRol
                                            && m.IdOperacion == IdOperacion
                                        select m;


                if (lstMisOperaciones.ToList().Count() == 0)
                {
                    var oOperacion = db.Tbl_Operaciones.Find(IdOperacion);
                    int? IdModulo = oOperacion.IdModulo;
                    NombreOperacion = GetNombreDeOperacion(IdOperacion);
                    NombreModulo = GetNombreDelModulo(IdModulo);
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + NombreOperacion); 
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + NombreOperacion);
            }
        }
        public string GetNombreDeOperacion(int IdOperacion)
        {
            var ope = from op in db.Tbl_Operaciones
                      where op.IdOperacion == IdOperacion
                      select op.NombreOperacion;
            String NombreOperacion;
            try
            {
                NombreOperacion = ope.First();
            }
            catch (Exception)
            {
                NombreOperacion = "";
            }
            return NombreOperacion;
        }

        public string GetNombreDelModulo(int? IdModulo)
        {
            var modulo = from m in db.Tbl_Modulos
                         where m.IdModulo == IdModulo
                         select m.NombreModulo;
            String NombreModulo;
            try
            {
                NombreModulo = modulo.First();
            }
            catch (Exception)
            {
                NombreModulo = "";
            }
            return NombreModulo;
        }

    }
}