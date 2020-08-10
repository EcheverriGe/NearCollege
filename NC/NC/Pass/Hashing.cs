using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NC.Pass
{
    public class Hashing
    {
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);

        }

        public static string HashPassword(string ContraseñaUsuario)
        {
            return BCrypt.Net.BCrypt.HashPassword(ContraseñaUsuario, GetRandomSalt());
        }

        public static bool ValidatePassword(string ContraseñaUsuario, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(ContraseñaUsuario, correctHash);
        }
    }
}