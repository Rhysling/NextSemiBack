using Microsoft.AspNetCore.Mvc;
using NextSemiBack.Recaptcha;
using System.Net;

namespace NextSemiBack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecaptchaController : ControllerBase
{
	private readonly RecaptchaService rs;

	public RecaptchaController(RecaptchaService rs)
	{
		this.rs = rs;
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> Verify([FromBody] RecaptchaToken token)
	{
		var rvr = await rs.VerifyAsync(token);

		if (rvr.Success)
			return Ok(rvr);

		return BadRequest(rvr);
	}

}