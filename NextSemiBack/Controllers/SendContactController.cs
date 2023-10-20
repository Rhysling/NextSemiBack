using Microsoft.AspNetCore.Mvc;
using NextSemiBack.Mailer;
using NextSemiBack.Models;
using NextSemiBack.Services;

namespace NextSemiBack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SendContactController : ControllerBase
{
	private readonly MailgunService mgs;
	private readonly AppSettings aps;
	private readonly ContactMessageDb db;

	public SendContactController(MailgunService mgs, AppSettings aps, ContactMessageDb db) {
		this.mgs = mgs;
		this.aps = aps;
		this.db = db;
	}

	// POST api/SendContact
	[HttpPost]
	public async Task<IActionResult> PostAsync([FromBody] ContactMessage msg, [FromQuery] String k)
	{
		if (k != "812g")
			return StatusCode(403);

		string tos = aps.NextSemi.IsProduction ? "contact@nextsemi.com" : "rpkummer@hotmail.com,rkummer@polson.com";

		ObjectResult ret;

		try
		{
			var res = await mgs.SendAsync(msg, tos);				 
			ret = StatusCode((int)res.StatusCode, res);
		}
		catch (Exception ex)
		{
			ret = StatusCode(500, ex.Message);
		}

		msg.SentAt = DateTime.Now.ToString("s");
		msg.StatusCode = ret.StatusCode ?? 0;
		db.Add(msg);			

		return ret;
	}
}
