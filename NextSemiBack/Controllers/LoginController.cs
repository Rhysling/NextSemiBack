using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NextSemiBack.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace NextSemiBack.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public partial class LoginController : ControllerBase
	{
		private readonly AppSettings aps;

		public LoginController(AppSettings aps)
		{
			this.aps = aps;
		}


		// POST api/<LoginController>
		[AllowAnonymous]
		[HttpPost]
		public IActionResult Post([FromBody] UserLogin login)
		{
			IActionResult response = Unauthorized();
			var user = AuthenticateUser(login);

			if (user != null)
			{
				var tokenString = GenerateJSONWebToken(user);
				user.Token = "Bearer " + tokenString;
				response = Ok(user);
			}

			return response;
		}


		private UserClient? AuthenticateUser(UserLogin login)
		{
			if (login.Email is null)
				return null;

			// isValidEmail = /^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$/.test(email);
			if (!EmailRegex().IsMatch(login.Email))
				return null;

			bool isAmin = login.Email.EndsWith("nextsemi.com");

			if (login.Pw != aps.NextSemi.AdminPw)
				return null;

			var uc = new UserClient {
				Email = login.Email,
				FullName = login.FullName,
				IsAdmin = isAmin
			};

			return uc;
		}

		private string GenerateJSONWebToken(UserClient user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(aps.Jwt.Key));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[] {
				new Claim("UserId", user.UserId.ToString()),
				new Claim("Email", user.Email),
				new Claim("FullName", user.FullName ?? ""),
				new Claim("IsAdmin", user.IsAdmin.ToString())
			};

			var token = new JwtSecurityToken(aps.Jwt.Issuer,
					aps.Jwt.Issuer,
					claims,
					//expires: DateTime.Now.AddSeconds(30),
					expires: DateTime.Now.AddDays(10),
					signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		[GeneratedRegex("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$")]
		private static partial Regex EmailRegex();
	}
}
