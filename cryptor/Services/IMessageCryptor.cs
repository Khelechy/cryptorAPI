using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cryptor.Services
{
	public interface IMessageCryptor
	{
		string Encrypt(string message, string key);

		string Decrypt(string message, string key);
	}
}
