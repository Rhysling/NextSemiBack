namespace NextSemiBack.Models
{
	public class TechItem
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Type { get; set; }
		public required string Description { get; set; }
		public int ProcessNode { get; set; }
		public int SampleRate { get; set; }
		public string? FileName { get; set; }
		}
}
