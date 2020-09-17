using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cryptor.Services
{
	public class MessageCryptor : IMessageCryptor
	{
		public string Encrypt(string message, string key)
		{
            byte[] iv = new byte[16];
            byte[] array;
            HashAlgorithm hash = MD5.Create();

            using (Aes aes = Aes.Create())
            {
                aes.Key = hash.ComputeHash(Encoding.UTF8.GetBytes(key));
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(message);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

		public string Decrypt(string message, string key)
		{
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(message);
            HashAlgorithm hash = MD5.Create();

            using (Aes aes = Aes.Create())
            {
                aes.Key = hash.ComputeHash(Encoding.UTF8.GetBytes(key));
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
