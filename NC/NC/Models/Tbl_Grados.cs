//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    // Esta clase es la de grados, aquí la institución se encarga de subir los grados disponibles y no disponibles en la institución 
    // consta con una conexión directa a la base de datos y su correspondiente tabla
    public partial class Tbl_Grados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Grados()
        {
            this.Tbl_Grados_Fichos = new HashSet<Tbl_Grados_Fichos>();
        }

        // Este parámetro se encarga de asignarle un id único a cada grado, no consta con ninguna validación ya que se da de forma 
        // automática y, no tienen acceso a él ningún tipo de usuario
        public int IdGrado { get; set; }

        // Este parámetro consta con 2 validaciones, required y maxlenght, cada una cumple con una función estricta,
        // una es obgligar al usuario a llenar este campo sí o sí, y la otra es un máximo de caracteres permitido 
        // este parámetro tiene como fin el designar el nombre del grado que será ingresado
        [Required(ErrorMessage = "¡Este campo es requerido si desea registrar el grado!")]
        [MaxLength(20, ErrorMessage = "¡La cantidad de caracteres máxima permitida para este campo es de: 20!")]
        public string NombreGrado { get; set; }

        // El siguiente parámetro consta de dos validaciones también, las mismas siendo exactos, required y maxlength, las cuales 
        // ya han sido explicadas sus funciones, el fin de este parámetro es designar la jornada del grado que será añadido
        [Required(ErrorMessage = "¡Este campo es requerido si desea registrar el grado!")]
        [MaxLength(6, ErrorMessage = "¡La cantidad de caracteres máxima permitida para este campo es de: 6!")]
        public string JornadaGrado { get; set; }

        // Este parámetro tiene como fin el específicar si el grado está o no disponible, consta con dos validaciones también 
        // que son, Required y, MaxLength
        [Required(ErrorMessage = "¡Este campo es requerido si desea registrar el grado!")]
        [MaxLength(13, ErrorMessage = "¡La cantidad de caracteres máxima permitida para este campo es de: 13!")]
        public string DisponibilidadCupoGrado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Grados_Fichos> Tbl_Grados_Fichos { get; set; }
    }
}
