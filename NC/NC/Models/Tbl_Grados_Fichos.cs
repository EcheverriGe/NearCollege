namespace NC.Models
{
    using System;
    using System.Collections.Generic;

    // Esta clase es una clase intermedia entre Tbl_Grados y Tbl_Fichos, no cumple una función específica en el proyecto 
    // o al menos visible a la vista, pero cabe decir que está conectada    
    public partial class Tbl_Grados_Fichos
    {
        public int IdGRadosFichos { get; set; }
        public Nullable<int> IdFicho { get; set; }
        public Nullable<int> IdGrado { get; set; }
    
        public virtual Tbl_Grados Tbl_Grados { get; set; }
        public virtual Tbl_Fichos Tbl_Fichos { get; set; }
    }
}
