namespace NC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    // Esta clase se encarga de añadir uno o varios usuarios nuevos al proyecto, tiene conexión directa con la base de datos y su 
    // respectiva tabla
    public partial class Tbl_Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Usuarios()
        {
            this.Tbl_Fichos = new HashSet<Tbl_Fichos>();
        }

        // Identifiación única de cada usuario, asignada de forma automática por el sitio web, no consta con validaciones debido a que
        // el sitio web la asigna de forma automática y, ningún usuario tiene acceso a esta
        public int IdUsuario { get; set; }

        // Nombre de cada usuario del sitio web y, consta con 3 validaciones, Required, que se encarga de hacer el campo obligatorio
        // sin importar qué, proporcionando un mensaje de error designado en caso de que este quiera ser omitido, MaxLength, 
        // se encarga de asignar un valor máximo permitido de caracteres, proporcionando un mensaje de error, si alguien llega a 
        // transgredir ese límite, y por último, MinLength, que a diferencia de MaxLength, esta validación, nos proporciona
        // un mínimo de caracteres permitido, también proporcionando un mensaje de error si alguien llegase a transgredir ese 
        // límite
        [Required(ErrorMessage = "¡Este campo es requerido si desea registrar un usuario!")]
        [MaxLength(15, ErrorMessage = "¡La cantidad de caracteres máxima permitida para este campo es de: 15!")]
        [MinLength(3, ErrorMessage = "¡La cantidad de caracteres mínima permitida para este campo es de: 3!")]
        public string NombreUsuario { get; set; }

        // Apellido con el cual será registrado el usuario en el sitio web, consta de tres validaciones, las cuales fueron 
        // explicadas con anterioridad
        [Required(ErrorMessage = "¡Este campo es requerido si desea registrar un usuario!")]
        [MaxLength(16, ErrorMessage = "¡La cantidad de caracteres máxima permitida para este campo es de: 16!")]
        [MinLength(3, ErrorMessage = "¡La cantidad de caracteres mínima permitida para este campo es de: 3!")]
        public string ApellidoUsuario { get; set; }

        // Correo con el que el usuario será registrado en el sitio web, y de necesitarlo, será donde le llegará un mensaje de recuperación
        // de constraseña, consta con tres validaciones pero, ahora hay una nueva, EmailAddress, esta validación se encarga de verificar 
        // si el mensaje ingresado en ese campo, consta como una dirección de correo electrónico
        [Required(ErrorMessage = "¡Este campo es requerido si desea registrar un usuario!")]
        [EmailAddress(ErrorMessage = "¡El dato recién ingresado no está validado como una dirección de correo electrónico!")]
        [MaxLength(55, ErrorMessage = "¡La cantidad de caracteres máxima permitida para este campo es de: 55!")]
        public string CorreoUsuario { get; set; }

        // Contraseña personal con la cual el usuario será registrado en el sitio web, consta de tres validaciones, las cuales fueron 
        // explicadas anteriormente, pero, en este caso, constamos con otra diferente DataType.Password, esta se encarga de darle 
        // una censura de puntos negros de cierta manera a la contraseña ingresa en tiempo real, permitiendo así, más seguridad 
        // y tranquilidad en el usuario, ya en la base de datos, se consta con un tipo de seguridad mucho más avanzada, y que protege 
        // y vela por los datos de nuestros usuarios, este método se llama: Hasheo
        [Required(ErrorMessage = "¡Este campo es requerido si desea registrar un usuario!")]
        [MaxLength(500, ErrorMessage = "¡La cantidad de caracteres máxima permitida para este campo es de: 500!")]
        [DataType(DataType.Password)]
        public string ContraseñaUsuario { get; set; }

        // Este parámetro se encarga de designar el rol del usuario a punto de registrar, no consta con ningún tipo de validación, 
        // ya que es un campo de selección y, el sistema obligará a ingresar este campo a la personada encargada del registro
        public Nullable<int> IdRol { get; set; }

        // Este parámetro es una función externa para la recuperación de la contraseña en caso de perdida del usuario, no consta con 
        // validaciones ya que, es imposible acceder a este de forma manual, incluso desde la base de datos, este consta de forma aparte, 
        // con un hasheo avanzado
        public string Token_Recovery { get; set; }

        // Este parámetro es una función externa para el hasheo de la contraseña y, brindar mayor seguridad en el sitio web, no tiene 
        // ninguna validación debido a que es imposible acceder a este vía manual, incluso desde la base de datos
        public string Sal { get; set; }

        public virtual Tbl_Roles Tbl_Roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Fichos> Tbl_Fichos { get; set; }
    }
}
