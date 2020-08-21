using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace NC.Models.ViewModel
{
    public class RecoveryViewModel
    {
        [EmailAddress(ErrorMessage = "¡El dato recién ingresado no está validado como una dirección de correo electrónico!")]
        [Required (ErrorMessage = "¡Si desea recuperar su contraseña, debe completar este campo!")]
        public string CorreoUsuario { get; set; }
    }
}