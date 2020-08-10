using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NC.Models.ViewModel
{
    public class RecoveryPassWordViewModel
    {
        public string Token { get; set; }
        [Required(ErrorMessage = "¡Este campo es requerido si desea restablecer su contraseña!")]
        [MaxLength(32, ErrorMessage = "La cantidad de caracteres máxima permitida para este campo es de: 32")]
        public string ContraseñaUsuario { get; set; }

        [Required(ErrorMessage = "¡Este campo es requerido si desea restablecer su contraseña!")]
        [Compare("ContraseñaUsuario", ErrorMessage = "Las contraseñas son incompatibles, por favor verifique los datos")]
        [MaxLength(32, ErrorMessage = "La cantidad de caracteres máxima permitida para este campo es de: 32")]
        public string ContraseñaUsuario2 { get; set; }
    }
}