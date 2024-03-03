using System.Security.Cryptography;
using System.Text;

namespace IcecreamMAUI.Api.Services
{
   public class PasswordService
   {
      private const int SaltSize = 10;

      public (string salt, string hasedPassword) GenerateSaltAndHash(string plainPassword)
      {
         if (string.IsNullOrWhiteSpace(plainPassword))
            throw new ArgumentNullException(nameof(plainPassword));

         var buffer = RandomNumberGenerator.GetBytes(10);
         var salt = Convert.ToBase64String(buffer);

         var bytes = Encoding.UTF8.GetBytes(plainPassword + salt);
         var hash = SHA256.HashData(bytes);

         var hashedPassword = Convert.ToBase64String(hash);

         return (salt, hashedPassword);
      }

      public bool AreEqual(string plainPassword, string salt, string hashedPassword)
      {
         var newHashedPassword = GenerateHashedPassword(plainPassword, salt);
         return newHashedPassword == hashedPassword;
      }

      private string GenerateHashedPassword(string plainPassword, string salt)
      {
         var bytes = Encoding.UTF8.GetBytes(plainPassword + salt);
         var hash = SHA256.HashData(bytes);

         return Convert.ToBase64String(hash);
      }
   }
}