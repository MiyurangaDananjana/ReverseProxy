using System.Security.Cryptography;

namespace App1.Helper
{
    public static class PasswordHasherHelper
    {
        private const int SaltSize = 16; // Size of the salt in bytes
        private const int HashSize = 32; // Size of the hash in bytes
        private const int Iterations = 10000; // Number of iterations for the hashing algorithm

        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt
            byte[] hash = Pbkdf2(password, salt, Iterations, HashSize);

            // Combine the salt and hash for storage
            byte[] hashBytesWithSalt = new byte[HashSize + SaltSize];
            Array.Copy(hash, 0, hashBytesWithSalt, 0, HashSize);
            Array.Copy(salt, 0, hashBytesWithSalt, HashSize, SaltSize);

            // Convert the combined bytes to a base64 string
            string hashString = Convert.ToBase64String(hashBytesWithSalt);

            return hashString;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Extract the salt from the stored hash string
            byte[] hashBytesWithSalt = Convert.FromBase64String(hashedPassword);
            byte[] hashBytes = new byte[HashSize];
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytesWithSalt, 0, hashBytes, 0, HashSize);
            Array.Copy(hashBytesWithSalt, HashSize, salt, 0, SaltSize);

            // Hash the provided password with the stored salt
            byte[] hash = Pbkdf2(password, salt, Iterations, HashSize);

            // Compare the computed hash with the stored hash
            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
                       password,
                       salt,
                       iterations,
                       HashAlgorithmName.SHA256))
            {
                return algorithm.GetBytes(outputBytes);
            }
        }
    }
}
