using System.Security.Cryptography;
using System.Text;

namespace Pandora.Server.WebServices.Authentification.Server.WebServices.Authentification.Services
{
    [Transient<IPasswordService>]
    public class PasswordService : IPasswordService
    {
        private SHA256 sha256;
        private Random random;

        public PasswordService()
        {
            sha256 = SHA256.Create();
            random = new Random(Environment.TickCount);
        }

        public string GenerateSalt(int length)
        {
            var byteArray = new byte[length];

            random.NextBytes(byteArray);

            return Convert.ToBase64String(byteArray);
        }

        public string HashPassword(string plainPassword, string salt)
        {
            var saltArray = Convert.FromBase64String(salt);
            var passwordByteArray = Encoding.Unicode.GetBytes(plainPassword);
            var passwordHashArray = sha256.ComputeHash(saltArray.Concat(passwordByteArray).ToArray());

            return Convert.ToBase64String(passwordHashArray);
        }
    }

    public interface IPasswordService
    {
        string GenerateSalt(int length);

        string HashPassword(string plainPassword, string salt);
    }
}
