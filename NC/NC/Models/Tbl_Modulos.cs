namespace NC.Models
{
    using System;
    using System.Collections.Generic;

    // Esta clase también consta con la conexión a base de datos y, su tabla designada, cumple con el papel de separar el proyecto 
    // en tres módulos y así, permitir los permisos de cada acción
    public partial class Tbl_Modulos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Modulos()
        {
            this.Tbl_Operaciones = new HashSet<Tbl_Operaciones>();
        }

        // Este parámetro se encarga de darle una identificación única al módulo, no consta con validaciones porque ningún usuario tiene
        // acceso a este, de querer añadir un nuevo módulo, tendría que hacerse directamente desde la base de datos
        public int IdModulo { get; set; }

        // Por otro lado, este parámetro se encarga de darle un nombre al módulo que se creará, tampoco consta con validaciones por lo 
        // explicado con anterioridad, sólo puede ser agregado un nuevo módulo desde su base de datos
        public string NombreModulo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Operaciones> Tbl_Operaciones { get; set; }
    }
}
