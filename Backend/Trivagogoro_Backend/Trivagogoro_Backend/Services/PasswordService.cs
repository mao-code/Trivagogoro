using System;
namespace Trivagogoro_Backend.Services
{
    public class PasswordService
    {
        public static bool ValidatePassword(string password, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashed);
        }

        public static string HashPassword(string password, string salt)
        {

            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public static string GenerateSalt()
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            return salt;
        }
    }
}

