using System.Security.Cryptography;
using System.Text;

namespace Cubos.Finance.Core
{
    public static class Encryption
    {
        /// <summary>
        ///     Representação de valor em base 64 (Chave Interna)
        ///     O Valor representa a transformação para base64 de
        ///     um conjunto de 32 caracteres (8 * 32 = 256bits)
        ///     A chave é: "Criptografias com Rijndael / AES"
        /// </summary>
        private const string CryptoKey =
            "b14ca5898a4e4133bbce2ea2315a1916";

        /// <summary>
        ///     Vetor de bytes utilizados para a criptografia (Chave Externa)
        /// </summary>
        private static readonly byte[] bIV =
        {
            0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18,
            0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC
        };

        /// <summary>
        ///     Metodo de criptografia de valor
        /// </summary>
        /// <param name="text">valor a ser criptografado</param>
        /// <returns>valor criptografado</returns>
        public static string Encrypt(this string text)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(CryptoKey);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(text);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }


        /// <summary>
        ///     Pega um valor previamente criptografado e retorna o valor inicial
        /// </summary>
        /// <param name="text">texto criptografado</param>
        /// <returns>valor descriptografado</returns>
        public static string Decrypt(this string text)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(text);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(CryptoKey);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}