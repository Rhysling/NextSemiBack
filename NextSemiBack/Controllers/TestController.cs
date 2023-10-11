using NextSemiBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NextSemiBack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
	private static readonly string[] Summaries = new[]
	{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	//private readonly ILogger<WeatherForecastController> _logger;
	//private readonly AppSettings aps;


	public TestController()
	{
		//_logger = logger;
		//this.aps = aps;
	}


	//[HttpGet("[action]")]
	//public IEnumerable<WeatherForecast> GetWeatherForecast()
	//{
	//	return Enumerable.Range(1, 5).Select(index => new WeatherForecast
	//	{
	//		Date = DateTime.Now.AddDays(index),
	//		TemperatureC = Random.Shared.Next(-20, 55),
	//		Summary = Summaries[Random.Shared.Next(Summaries.Length)]
	//	})
	//	.ToArray();
	//}

	//[HttpGet("[action]")]
	//public AppSettings GetAppSettings()
	//{
	//	return aps;
	//}

	[HttpGet("[action]")]
	public string GetUnsecuredValue()
	{
		return "This is an unsecured value.";
	}

	[HttpGet("[action]")]
	[Authorize()]
	public string GetSecuredValue()
	{
		//var user = HttpContext.User;
		//var claims = user.Claims;
		//var x = user.Identity;
		//var y = 2;

		return "This is a secured value.";
	}

	[HttpGet("[action]")]
	[Authorize(Roles = "Admin")]
	public string GetAdminValue()
	{
		//var user = HttpContext.User;
		//var claims = user.Claims;
		//var x = user.Identity;
		//var y = 2;

		return "This is an admin value.";
	}

	[HttpGet("[action]")]
	public ActionResult Throw()
	{
		throw new Exception("Boom!");

		//return View();
	}

}
