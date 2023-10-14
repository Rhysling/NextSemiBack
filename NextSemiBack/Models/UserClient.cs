using NextSemiBack.Services.FiltersAttributes;

namespace NextSemiBack.Models;

[TypeScriptModel]
public class UserClient
{
	public int UserId { get; set; }
	public required string Email { get; set; }
	public required string FullName { get; set; }
	public string? Token { get; set; }
	public bool IsAdmin { get; set; }
}
