namespace NC.Models
{
    using System;
    using System.Collections.Generic;

    // Esta clase es una tabla intermedia entre Tbl_Roles y Tbl_Operaciones, en la cual se le asignará el número de la operación que se le
    // permitirá a cada rol, esta clase consta con conexión directa a la base de datos y su respectiva tabla
    public partial class Tbl_Roles_Operaciones
    {
        // El parámetro consta con validaciones debido a que ningún usuario puede acceder a esta, la única manera de acceder, es vía la base
        // de datos, este tiene el fin de darle una identificación única a su registro, se hace de forma manual
        public int IdRolOperacion { get; set; }

        // Como ya se mencionó, este parámetro no consta con validaciones, su fin es el de seleccionar qué rol tiene acceso a
        // las operaciones 
        public Nullable<int> IdRol { get; set; }

        // Este parámetro tampoco consta con validaciones por lo ya mencionado, y su fin es el de poner todas y cada una de las operaciones
        // creadas
        public Nullable<int> IdOperacion { get; set; }

        public virtual Tbl_Operaciones Tbl_Operaciones { get; set; }
        public virtual Tbl_Roles Tbl_Roles { get; set; }
    }
}
