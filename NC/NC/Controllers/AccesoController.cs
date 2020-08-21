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

namespace NC.Controllers
{
    public class AccesoController : Controller
    {
        string urlDomain = "https://localhost:44303/";

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string CorreoUsuario, string ContraseñaUsuario)
        {
            try
            {
                using (NCEntities db = new NCEntities())
                {
                    var lst = from d in db.Tbl_Usuarios
                                where d.CorreoUsuario == CorreoUsuario && d.ContraseñaUsuario == ContraseñaUsuario
                                select d;
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
            catch (Exception ex)
            {
                return Content("Ocurrió un error" + ex.Message);
            }
        }

        [HttpGet]
        public ActionResult StartRecovery()
        {
            Models.ViewModel.RecoveryViewModel model = new Models.ViewModel.RecoveryViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult StartRecovery(Models.ViewModel.RecoveryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string Token = GetSha256(Guid.NewGuid().ToString());

                using (Models.NCEntities db = new Models.NCEntities())
                {
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Recovery(string Token)
        {
            Models.ViewModel.RecoveryPassWordViewModel model = new Models.ViewModel.RecoveryPassWordViewModel();
            model.Token = Token;
            using (Models.NCEntities db=new Models.NCEntities())
            {
                if(model.Token==null || model.Token.Trim().Equals(""))
                {
                    return View("Login");
                }
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

                using (Models.NCEntities db= new Models.NCEntities())
                {
                    var oUser = db.Tbl_Usuarios.Where(d => d.Token_Recovery == model.Token).FirstOrDefault();

                    if (oUser != null)
                    {
                        oUser.ContraseñaUsuario = model.ContraseñaUsuario;
                        oUser.Token_Recovery = null;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Message = "Contraseña modificada con éxito";
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View("Login");
        }


        #region HELPERS
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