using System.Security.Cryptography;
using System.Text;

namespace Mcm.Shared.Domain.Extensions
{
    public class EncryptationExtension
    {
        private static readonly string key = "loremipsumdolors";
        public static string Encrypt(string plaintext)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream ms = new();
            using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter sw = new(cs))
                {
                    sw.Write(plaintext);
                }
            }
            return Convert.ToBase64String(ms.ToArray());
        }

    // // Method to decrypt data
    //     public static string Decrypt(string ciphertext, string key)
    //     {
    //         using Aes aes = Aes.Create();
    //         aes.Key = Encoding.UTF8.GetBytes(key);
    //         aes.IV = new byte[16]; // Initialization vector (IV)

    //         // Create a decryptor to perform the stream transform
    //         ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

    //         // Create the streams used for decryption
    //         using MemoryStream ms = new(Convert.FromBase64String(ciphertext));
    //         using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
    //         using StreamReader sr = new(cs);
    //         return sr.ReadToEnd();
    //     }
    }
}