namespace NC.Models
{
    using System;
    using System.Collections.Generic;

    // Esta clase nos permite crear un nuevo rol específico, tiene conexión directa con la base de datos y, su respectiva tabla
    public partial class Tbl_Roles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Roles()
        {
            this.Tbl_Roles_Operaciones = new HashSet<Tbl_Roles_Operaciones>();
            this.Tbl_Usuarios = new HashSet<Tbl_Usuarios>();
        }

        // Este parámetro consta con cero validaciones ya que ningún usuario corriente puede acceder a esta, de no ser por la base de datos
        // tiene el fin de darle un id único al rol y, lo asigna de forma automática 
        public int IdRol { get; set; }

        // En este apartado sucede exactamente lo mismo explicado con anterioridad y, tiene el fin de darle un nombre al nuevo rol a 
        // crear
        public string NombreRol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Roles_Operaciones> Tbl_Roles_Operaciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Usuarios> Tbl_Usuarios { get; set; }
    }
}
