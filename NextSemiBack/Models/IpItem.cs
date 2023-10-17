namespace NextSemiBack.Models
{
	public class IpItem
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Category { get; set; }
		public required string Description { get; set; }
		public required string Resolution { get; set; }
		public required string Speed { get; set; }
		public required string Technology { get; set; }
		public string? FileName { get; set; }
		}
}
