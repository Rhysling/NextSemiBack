using NextSemiBack.Services.FiltersAttributes;

namespace NextSemiBack.Models
{
	[TypeScriptModel]
	public class UserRegister : UserLogin
	{
		public required string FullName { get; set; }
	}
}
