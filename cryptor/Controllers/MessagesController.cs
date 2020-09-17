using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptor.Models;
using cryptor.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cryptor.Controllers
{
	[Route("api/message")]
	[ApiController]
	public class MessagesController : ControllerBase
	{
		private readonly IMessageCryptor _messageCryptor;

		public MessagesController(IMessageCryptor messageCryptor)
		{
			_messageCryptor = messageCryptor;
		}


		[HttpGet("encrypt")]
		public IActionResult Encrypt([FromBody] Message model)
		{
			var result = _messageCryptor.Encrypt(model.Text, model.Key);
			return Ok(result);
		}


		[HttpGet("decrypt")]
		public IActionResult Decrypt([FromBody] Message model)
		{
			var result = _messageCryptor.Decrypt(model.Text, model.Key);
			return Ok(result);
		}
	}
}
