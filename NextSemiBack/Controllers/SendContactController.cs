﻿using Microsoft.AspNetCore.Mvc;
using NextSemiBack.Mailer;
using NextSemiBack.Models;

namespace NextSemiBack.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SendContactController : ControllerBase
	{
		private readonly MailgunTarget mgt;
		private readonly AppSettings aps;

		public SendContactController(MailgunTarget mgt, AppSettings aps) {
			this.mgt = mgt;
			this.aps = aps;
		}

		// POST api/SendContact
		[HttpPost]
		public async Task<IActionResult> PostAsync([FromBody] ContactMessage msg, [FromQuery] String k)
		{
			if (k != "812g")
				return StatusCode(403);

			string tos = aps.NextSemi.IsProduction ? "bob@nextsemi.com" : "rpkummer@hotmail.com,rkummer@polson.com";

			try
			{
				var res = await mgt.SendAsync(msg, tos);

				if (res.IsSuccessStatusCode) return Ok();

				return StatusCode((int)res.StatusCode, res);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}


		}
	}
}