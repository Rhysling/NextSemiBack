using NextSemiBack.Services.FiltersAttributes;

namespace NextSemiBack.Models;

[TypeScriptModel]
public class UserLogin
{
	public required string Email { get; set; }
	public required string Pw { get; set; }
}
