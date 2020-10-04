namespace NC.Models
{
    using System;
    using System.Collections.Generic;

    // Esta clase se encarga de monitorear, agregar, eliminar, editar cada función del proyecto para los usuarios, cabe decir 
    // que también constá con una  conexión a la base de datos y, su respectiva tabla
    public partial class Tbl_Operaciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Operaciones()
        {
            this.Tbl_Roles_Operaciones = new HashSet<Tbl_Roles_Operaciones>();
        }

        // Este parámetro da una identificación única a la operación, no consta con validaciones porque ningún usuario tiene acceso a ella
        // la única forma de modificar o acceder algo relacionado con este parámetro es directamente desde la base de datos
        public int IdOperacion { get; set; }

        // Este parámetro se encarga de darle el nombre que usted desee a la operación, no consta con validaciones por lo ya mencionado
        public string NombreOperacion { get; set; }

        // Este parámetro es una ForeignKey de la tabla Tbl_Modulos, no consta con validaciones por lo ya mencionado y, es usado para 
        // designar en qué módulo va cada operación o acción que se crea nueva
        public Nullable<int> IdModulo { get; set; }

        public virtual Tbl_Modulos Tbl_Modulos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Roles_Operaciones> Tbl_Roles_Operaciones { get; set; }
    }
}
