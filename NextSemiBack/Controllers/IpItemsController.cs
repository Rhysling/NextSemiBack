using BotanicaStoreBack.Services.FiltersAttributes;
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

	// POST: api/IpItems/Save
	[AdminAuthorize]
	[HttpPost("[action]")]
	public IActionResult Save([FromBody] IpItem item)
	{
		db.SaveItem(item);
		return Created("/Save", item);
	}

	// POST: api/IpItems/Delete
	[AdminAuthorize]
	[HttpPost("[action]")]
	public IActionResult Delete([FromBody] IpItem item)
	{
		db.DeleteItem(item);
		return Ok();
	}
}
