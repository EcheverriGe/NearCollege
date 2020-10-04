namespace NC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    // Esta clase es usada para que el acudiente haga la reserva del ficho a la hora y fecha designados, cabe decir que todos y cada 
    // uno de los parámetros tienen una conexión directa con la base de datos, la tabla Tbl_Fichos y, acceso a los datos completamente de 
    // esta tabla
    public partial class Tbl_Fichos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Fichos()
        {
            this.Tbl_Grados_Fichos = new HashSet<Tbl_Grados_Fichos>();
        }

        // Este parámetro contiene un id único que es asignado automáticamente, no consta con validaciones porque el usuario no puede
        // acceder a este
        public int IdFicho { get; set; }

        // Este parámetro se encarga de seleccionar la fecha en la que el usuario hará la reservación del ficho, consta con de dos
        // validaciones, Required, mencionada en comentarios anteriores, que es la que se utiliza para hacer un campo obligatorio
        // con un mensaje designado al gusto, y, DataType.Date, que es la que se encarga de darnos el formato tipo fecha
        [Required(ErrorMessage = "¡Este campo es requerido si desea reservar el ficho!")]
        [DataType(DataType.Date)]
        public System.DateTime FechaFicho { get; set; }

        // Este parámetro se encarga de escoger el usuario del ficho, consta con una simple validación, y es la de required, explicada
        // en anteriores comentarios
        [Required(ErrorMessage = "¡Este campo es requerido si desea reservar el ficho!")]
        public Nullable<int> IdUsuario { get; set; }

        // Este parámetro se encarga de seleccionar la hora en la que el acudiente reservará el ficho, consta con una varias validaciones y, 
        // una de ellas es la ya mencionada Required, las otras se encargan de validar la hora
        [Required(ErrorMessage = "¡Este campo es requerido si desea reservar el ficho!")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public string HoraFicho { get; set; }

        public virtual Tbl_Usuarios Tbl_Usuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Grados_Fichos> Tbl_Grados_Fichos { get; set; }

    }
}
