using System.Security.Cryptography;
using System.Text;

namespace LB.Utility.Encryption
{
    public class Asymmetric
    {
        public static String CreateHash(String value)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
