using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NC.Pass
{
    // Esta clase como lo mencioné en anteriores casos, se encarga de hashear / censurar / generar caracteres aleatorios a la contraseña del 
    // usuario, para así proporcionarle una mayor seguridad y experiencia en el sitio web mientras esté en él
    public class Hashing
    {
        //  Este método se encarga de generarnos un "Sal" aleatorio para la constraseña que acabamos de ingresar 
        private static string GetRandomSalt()
        {
            // Una vez generado el salt, nos retornará el mencionado, habiéndolo generado de aproximadamente 12 caracteres o más
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        // Este método ya se encargaría de generar el Hash y reemplazarlo con el atributo ContraseñaUsuario, el cual se encuentra conectado
        // a la base de datos y, a su respectiva tabla Tbl_Usuarios en tiempo real
        public static string HashPassword(string ContraseñaUsuario)
        {
            // En este momento, una vez se haya generado el Hash, nos lo retornará y, reemplazará la contraseña, exponiendo visualmente 
            // el hash, con sus caracteres aleatorios, en lugar de la contraseña ingresada por el usuario
            return BCrypt.Net.BCrypt.HashPassword(ContraseñaUsuario, GetRandomSalt());
        }

        // En este último paso, lo que hará el método, será verificar y validar tanto la contraseña, como el hash generado
        public static bool ValidatePassword(string ContraseñaUsuario, string correctHash)
        {
            // Como paso final, internamente se validará el contenido ingresado por el usuario (Contraseña), el contenido generado por el
            // sistema (Hash) y, verificará los datos, si estos son correctos, los imprimirá en sus respestivos campos en la base de datos 
            // como se mencionaba anteriormente, el hash reemplazando a la contraseña ingresada por el usuario, brindándole así, más 
            // seguridad, confianza y, una mejor experiencia de navegación
            return BCrypt.Net.BCrypt.Verify(ContraseñaUsuario, correctHash);
        }
    }
}