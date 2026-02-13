using System.Security.Cryptography;
using System.Text;

namespace LB.Utility.Encryption
{
    public class Symmetric
    {
        readonly Aes aes;

        public Symmetric(byte[] key, byte[] iv)
        {
            aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
        }

        public String Encrypt(String value)
        {
            var encryptor = aes.CreateEncryptor();

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(value); // Write all data to the stream.
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public String Decrypt(String value) => Decrypt(Convert.FromBase64String(value));

        public String Decrypt(byte[] value)
        {
            var decryptor = aes.CreateDecryptor();

            using var ms = new MemoryStream(value);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}