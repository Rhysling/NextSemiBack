using Microsoft.AspNetCore.Mvc;
using NextSemiBack.Mailer;
using NextSemiBack.Models;

namespace NextSemiBack.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SendContactController : ControllerBase
	{
		private readonly MailgunService mgs;
		private readonly AppSettings aps;

		public SendContactController(MailgunService mgs, AppSettings aps) {
			this.mgs = mgs;
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
				var res = await mgs.SendAsync(msg, tos);

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
