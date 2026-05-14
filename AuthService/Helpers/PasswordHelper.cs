using System.Security.Cryptography;
using System.Text;

namespace AuthService.Helpers
{
    public static class PasswordHelper
    {
        // Generates a random salt
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        // Hashes the password combined with the salt
        public static string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            string combined = password + salt;
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
            return Convert.ToBase64String(hashBytes);
        }

        // Verifies entered password against stored hash
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            string hashOfEntered = HashPassword(enteredPassword, storedSalt);
            return hashOfEntered == storedHash;
        }
    }
}