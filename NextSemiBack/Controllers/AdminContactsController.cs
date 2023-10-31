using BotanicaStoreBack.Services.FiltersAttributes;
using Microsoft.AspNetCore.Mvc;
using NextSemiBack.Services;

namespace NextSemiBack.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminContactsController : ControllerBase
	{
		private readonly ContactMessageDb db;

		public AdminContactsController(ContactMessageDb db)
		{
			this.db = db;
		}

		// GET api/AdminContacts/GetAll
		[AdminAuthorize]
		[HttpGet("[action]")]
		public IActionResult GetAll()
		{
			return Ok(db.Items.OrderByDescending(a => a.SentAt).ToList());
		}
	}
}
