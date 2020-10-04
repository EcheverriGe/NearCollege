using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace NC.Models.ViewModel
{
    // En esta clase el usuario deberá ingresar el correo electrónico con el que fue registrado en el sitio web si es que quiere 
    // recuperar o restablecer su contraseña, cabe decir que el parámetro, campo CorreoUsuario está conectado directamente con 
    // la base de datos, con la tabla Tbl_Usuario, por tanto, tiene acceso a los datos de esa tabla
    public class RecoveryViewModel
    {
        // El parámetro consta con dos validaciones, una de Required, la cual se encarga de hacer que este campo sea obligatorio sí o sí, 
        // implementando un mensaje de error llegado el caso de que el usuario quiera omitirlo, y, una validación de dato, que en este caso
        // es EmailAddress, para un correo electrónico, también con un mensaje de error si el usuario no ingresa el correo, o simplemente 
        // llena un campo no validado como dirección de correo electrónico
        [EmailAddress(ErrorMessage = "¡El dato recién ingresado no está validado como una dirección de correo electrónico!")]
        [Required (ErrorMessage = "¡Si desea recuperar su contraseña, debe completar este campo!")]
        public string CorreoUsuario { get; set; }
    }
}