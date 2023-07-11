using System.Security.Cryptography;

namespace SistemaAsociados.Utils
{
    public class HashPassword
    {
        public static string CreateSHAHash(string Phrase)
        {
            SHA512 HashTool = new SHA512Managed();
            Byte[] PhraseAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(Phrase));
            Byte[] EncryptedBytes = HashTool.ComputeHash(PhraseAsByte);
            HashTool.Clear();
            return Convert.ToBase64String(EncryptedBytes);
        }
    }
}
