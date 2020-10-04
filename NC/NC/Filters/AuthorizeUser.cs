using NC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NC.Filters
{
    // Es un Atributo personalizado en el cual con  AttrubuteTargets. Method nos indica que se puede aplicar en cualquier método 
    // y allowmultiple=false nos dice que solo una instancia con el valor predeterminado
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]

    // Esta clase cumple la función de iniciar una autorización según el rol de cada usuario
    public class AuthorizeUser : AuthorizeAttribute
    {
        // Este método es privado y, hace referencia a que Tbl_Usuarios va a ser identificado con oUsuario
        private Tbl_Usuarios oUsuario;

        // Este método es privado también y, es la conexión con la base de datos
        private NCEntities1 db = new NCEntities1();

        // Aquí estamos definiendo como parámetro privado a IdOperacion
        private int IdOperacion;

        // Aquí inicializamos IdOperacion en 0, para evitar inconvenientes con el id de las otras funciones
        public AuthorizeUser(int IdOperacion = 0)
        {
            // Y, aquí hacemos referencia a que, this.IdOperacion, es el mismo que hay en la base de datos llamado IdOperacion
            // haciendo así, una conexión
            this.IdOperacion = IdOperacion;
        }

        // Aquí es donde se valida o activamos las funciones como la autorización de cada módulo y los filtros
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string NombreOperacion = "";
            string NombreModulo = "";

            // Esta línea de código define que, oUsuario sea la sesión actual en el sitio web, entrando a la base de datos, a la tabla
            // Tbl_Usuarios definiendo como tal que, de esa tabla, el atributo, aparte, define que lstMisOperaciones está conectada
            // Con la base de datos y, la tabla Tbl_Roles_Operaciones, donde cada cambio o modificación, se ejecutará en ambos sitios
            try
            {
                oUsuario = (Tbl_Usuarios)HttpContext.Current.Session["CorreoUsuario"];
                var lstMisOperaciones = from m in db.Tbl_Roles_Operaciones
                                        where m.IdRol == oUsuario.IdRol
                                            && m.IdOperacion == IdOperacion
                                        select m;

                // Primordialmente, en el if se está validando la información de los parámetros y, la base de datos, para que estén
                // sincronizados todos los datos, por otro lado, este if lo que hace es que, en caso de que la id de la operación sea 
                // igual a 0, marcará un error, mostrando así el nombre de la operación que el usuario quería ejecutar
                if (lstMisOperaciones.ToList().Count() == 0)
                {
                    var oOperacion = db.Tbl_Operaciones.Find(IdOperacion);
                    int? IdModulo = oOperacion.IdModulo;
                    NombreOperacion = GetNombreDeOperacion(IdOperacion);
                    NombreModulo = GetNombreDelModulo(IdModulo);
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + NombreOperacion); 
                }
            }

            // Por otro lado, la función de este catch, es interceptar cualquier error posible y así, anunciarle al usuario 
            // qué tipo de error ha sido, y el nombre de la operación
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + NombreOperacion);
            }
        }

        // Esta clase la única función que está cumpliendo es sincronizar el nombre de cada una de las funciones y operaciones que están
        // registradas en la base de datos, con las variables, haciendo así la conexión con la base de datos y, conectando los parámetros,
        // con sus respectivas tablas e información
        public string GetNombreDeOperacion(int IdOperacion)
        {
            var ope = from op in db.Tbl_Operaciones
                      where op.IdOperacion == IdOperacion
                      select op.NombreOperacion;
            String NombreOperacion;

            // Este try tiene una función parecida al de la anterior, se basa en dar un nombre, el nombre de la operación 
            // se lo proporciona a ope.First()
            try
            {
                NombreOperacion = ope.First();
            }

            // El catch se encarga de imprimirnos el error y nombre de la operación al mismo tiempo
            catch (Exception)
            {
                NombreOperacion = "";
            }

            // Aquí por lógca, nos va a retornar el nombre de la operación
            return NombreOperacion;
        }

        // Esta clase la única función que está cumpliendo es sincronizar el nombre de cada una de las funciones y módulos que están
        // registradas en la base de datos, con las variables, haciendo así la conexión con la base de datos y, conectando los parámetros,
        // con sus respectivas tablas e información
        public string GetNombreDelModulo(int? IdModulo)
        {
            var modulo = from m in db.Tbl_Modulos
                         where m.IdModulo == IdModulo
                         select m.NombreModulo;
            String NombreModulo;

            // Este try tiene una función parecida al de la anterior, se basa en dar un nombre, el nombre del módulo 
            // se lo proporciona a modulo.First()
            try
            {
                NombreModulo = modulo.First();
            }

            // El catch se encarga de imprimirnos el error y nombre del módulo al mismo tiempo
            catch (Exception)
            {
                NombreModulo = "";
            }

            // Aquí por lógca, nos va a retornar el nombre del módulo
            return NombreModulo;
        }

    }
}