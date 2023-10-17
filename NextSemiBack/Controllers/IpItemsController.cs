using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextSemiBack.Models;
using NextSemiBack.Services;

namespace NextSemiBack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IpItemsController : ControllerBase
{
	private readonly IpItemDb db;


	public IpItemsController(IpItemDb db)
	{
		this.db = db;
	}

	[HttpGet]
	public List<IpItem> Get()
	{
		return db.Items;
	}

	[HttpPost("[action]")]
	//[Authorize(Roles = "Admin")]
	// /api/IpItems/Save
	public IActionResult Save([FromBody] IpItem item)
	{
		db.SaveItem(item);
		return Created("/Save", item);
	}

	[HttpPost("[action]")]
	//[Authorize(Roles = "Admin")]
	public IActionResult Delete([FromBody] IpItem item)
	{
		db.DeleteItem(item);
		return Ok();
	}
}
