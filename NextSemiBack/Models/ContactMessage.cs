using NextSemiBack.Services.FiltersAttributes;

namespace NextSemiBack.Models
{
	[TypeScriptModel]
	public class ContactMessage
	{
		public required string Name { get; set; }
		public required string Email { get; set; }
		public required string Company { get; set; }
		public required string Phone { get; set; }
		public required string Message { get; set; }
		public string? SentAt { get; set; }
		public int StatusCode { get; set; }
	}

	
}
