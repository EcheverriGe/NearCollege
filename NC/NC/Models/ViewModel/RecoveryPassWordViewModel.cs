using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NC.Models.ViewModel
{
    // En esta clase es en la que el usuario podrá recuperar su contraseña de forma exitosa, siempre y cuando cumpla con todos 
    // los requerimientos de cada campo, está compuesto por 3 parámetros y sus respectivas validaciones
    public class RecoveryPassWordViewModel
    {
        // Este parámetro es del token hash, no tiene ninguna validación debido a que se da automáticamente y, como ya mencioné
        // está hasheado
        public string Token { get; set; }

        // El parámetro ContraseñaUsuario está conectado directamente con la tabla Tbl_Usuarios, con su atributo ContraseñaUsuario
        // y, tiene dos validaciones, una tipo Required, que es la que se encarga de que el campo sea obligatorio sí o sí, con un mensaje
        // de error y, una MaxLength, que se encarga de permitir un máximo de carácteres, con un mensaje de error también
        [Required(ErrorMessage = "¡Este campo es requerido si desea restablecer su contraseña!")]
        [MaxLength(32, ErrorMessage = "¡La cantidad de caracteres máxima permitida para este campo es de: 32!")]
        public string ContraseñaUsuario { get; set; }

        // El parámetro ContraseñaUsuario2 es una confirmación de, pero no está conectado con ninguna tabla, ni con la base de datos
        // pero, tiene una validación, que se llama Compare, que, con esta, a pesar de no estar en la base de datos, tiene que tener 
        // los mismos carácteres del campo ContraseñaUsuario, sin importar qué, exactamente igual, y, también consta con las dos 
        // validaciones de Required y MaxLength explicadas anteriormente
        [Required(ErrorMessage = "¡Este campo es requerido si desea restablecer su contraseña!")]
        [Compare("ContraseñaUsuario", ErrorMessage = "¡Las contraseñas son incompatibles, por favor verifique los datos!")]
        [MaxLength(32, ErrorMessage = "¡La cantidad de caracteres máxima permitida para este campo es de: 32!")]
        public string ContraseñaUsuario2 { get; set; }
    }
}