using NC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Xml.Linq;
using System.IO;
using Org.BouncyCastle.Crypto.Tls;

namespace NC.Controllers
{
    // Clase para el acceso al sitio web
    public class AccesoController : Controller
    {
        // Se define la URL del sitio web, función para el recuperar contraseña
        string urlDomain = "https://localhost:44303/";

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        // Aquí para el inicio de sesión, el método login, vamos a autenticarlo usando el string del Correo y, el string de la Contraseña
        public ActionResult Login(string CorreoUsuario, string ContraseñaUsuario)
        {
            try
            {
                // Aquí se hace la conexión a la base de datos
                using (NCEntities db = new NCEntities())
                {
                    // Y en estas 3 líneas, se hacen las validaciones
                    var lst = from d in db.Tbl_Usuarios
                                where d.CorreoUsuario == CorreoUsuario && d.ContraseñaUsuario == ContraseñaUsuario
                                select d;
                    // Mientras que aquí, estamos mirando que, si la cantidad de usuarios es mayor a 0, entonces existe un usuario
                    if (lst.Count()>0)
                    {

                        Tbl_Usuarios oUser = lst.First();
                        Session["CorreoUsuario"] = oUser;
                        return Content("1");
                    }
                    else
                    {
                        return Content("Correo o contraseña incorrectos");
                        
                    }
                }

            }
            // Aquí recibimos los errores en caso de haber alguno
            catch (Exception ex)
            {
                return Content("Ocurrió un error" + ex.Message);
            }
        }

        [HttpGet]
        // Aquí como el nombre lo indica, se empieza la recuperación de la contraseña, en caso de necesitarlo
        public ActionResult StartRecovery()
        {
            Models.ViewModel.RecoveryViewModel model = new Models.ViewModel.RecoveryViewModel();
            return View(model);
        }

        [HttpPost]
        // Aquí se continúa con la recuperación de contraseña, que, si el modelo es válido, nos retorne al apartado con su respectiva vista
        public ActionResult StartRecovery(Models.ViewModel.RecoveryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Aquí se genera un token hasheado, usando la conexión con la base de datos para albergarlo en su respectivo campo
                string Token = GetSha256(Guid.NewGuid().ToString());

                using (Models.NCEntities db = new Models.NCEntities())
                {
                    // Aquí se hace la validación en la base de datos del correo ingresado, y con el que 
                    // fue registrado, para así mandarle el correo con la recuperación
                    var oUser = db.Tbl_Usuarios.Where(d => d.CorreoUsuario == model.CorreoUsuario).FirstOrDefault();
                    if (oUser != null)
                    {
                        oUser.Token_Recovery = Token;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        //Enviar correo
                        SendEmail(oUser.CorreoUsuario, Token);
                    }
                }

                return View();
            }
            // Aquí se muestran los errores en general
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Recovery(string Token)
        {
            // Aquí es donde una vez se está restableciendo la contraseña, se hace la validación en la base de datos
            // Se compara el token, y la contraseña se restablece a la designada por el usuario
            Models.ViewModel.RecoveryPassWordViewModel model = new Models.ViewModel.RecoveryPassWordViewModel();
            model.Token = Token;
            using (Models.NCEntities db=new Models.NCEntities())
            {
                if(model.Token==null || model.Token.Trim().Equals(""))
                {
                    return View("Login");
                }
                
                // Aquí se hace la validación de si se llega a utilizar un token antiguo, pues que le resaltará un error
                var oUser = db.Tbl_Usuarios.Where(d => d.Token_Recovery == model.Token).FirstOrDefault();
                if (oUser== null)
                {
                    ViewBag.Error = "Tu token ha expirado";
                    return View("Login");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Recovery(Models.ViewModel.RecoveryPassWordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Aquí más de lo mismo, hacemos la validación de NCEntities con la base de datos para
                // comparar los tokens y ver si son los mismo o, ha sido utilizado con anterioridad
                using (Models.NCEntities db= new Models.NCEntities())
                {
                    // Se hace la validación del token, para ver si ha sido usado o no
                    var oUser = db.Tbl_Usuarios.Where(d => d.Token_Recovery == model.Token).FirstOrDefault();

                    if (oUser != null)
                    {
                        // En las siguientes líneas de código, se entra al campo de la contraseña de usuario, se hace uso del token
                        // Se actualizan los datos y, se guardan los cambios hechos
                        // Mostrando un mensaje de éxito, en caso de haber hecho todo correctamente
                        oUser.ContraseñaUsuario = model.ContraseñaUsuario;
                        oUser.Token_Recovery = null;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Message = "Contraseña modificada con éxito";
                    }
                }

            }
            // Aquí se muestran los errores en general
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View("Login");
        }


        // Este apartado es para los helpers
        #region HELPERS
        // Aquí está el apartadode Sha256, que se usó en el token y la contraseña
        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        // Y aquí es donde se proporciona el correo de recuperación de la contraseña, con un mensaje designado por defecto, y 
        // El link del token
        private void SendEmail(string EmailDestino, string Token)
        {
            string EmailOrigen = "nearcollege0405@gmail.com";
            string Contraseña = "es1001250012cc..";
            string url = urlDomain+"/Acceso/Recovery/?Token="+Token;

            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperación de Contraseña [NearCollege]",
                "<p>Cordial saludo, recientemente hemos recibido una solicitud para el restablecimiento de la contraseña de su cuenta registrada en nuestro sistema (NearCollege). Si realmente es usted quien realizó esta solicitud, haga click en el siguiente enlace para la asignación por usted mismo de su nueva contraseña, de lo contrario, le pedimos una disculpa e ignore este mensaje, que tenga un buen día. ¡ESTE MENSAJE ES GENERADO DE MANERA AUTOMÁTICA, POR TANTO, NO DEBE SER RESPONDIDO, DE HACERLO, NO HABRÁ RESPUESTA ALGUNA POR PARTE DEL SOPORTE DE NEARCOLLEGE!.</p><br>"+
                "<a href='"+url+"'>¡Has click para restablecer tu contraseña!</a>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);

            oSmtpClient.Send(oMailMessage);

            oSmtpClient.Dispose();
        }

        #endregion
    }
}