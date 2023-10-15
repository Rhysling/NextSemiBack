using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using NextSemiBack.Models;

namespace NextSemiBack.Services
{
	public class TechItemDb
	{
		private List<TechItem> db;
		private string filePath;

		public TechItemDb(AppSettings aps)
		{
			string dir = Directory.GetCurrentDirectory();
			//D:\UserData\Documents\AppDev\NextSemiBack\NextSemiBack
			if (aps.NextSemi.IsProduction)
				filePath = Path.Combine(dir, @"wwwroot\docs\TechItemDb.json");
			else
				filePath = dir.Replace(@"NextSemiBack\NextSemiBack", @"NextSemiFront\public\docs\TechItemDb.json");

			if (File.Exists(filePath)) { 
				string json = File.ReadAllText(filePath);
				db = JsonConvert.DeserializeObject<List<TechItem>>(json) ?? new List<TechItem>();
			}
			else
				db = new List<TechItem>();

		}
		public string FilePath => filePath;

		public List<TechItem> Items => db;	

		public void SaveItem(TechItem item)
		{
			db = db.Where(a => a.Id != item.Id).ToList();
			db.Add(item);
			File.WriteAllText(FilePath, JsonConvert.SerializeObject(db));
		}

		public void Create()
		{
			int nextId = db.Max(a => a.Id) + 1;

			var item = new TechItem {
				Id = nextId,
				Name = $"Thing number {nextId}",
				Type = "ADC",
				Description = $"This is a valuable thing times {nextId}!",
				ProcessNode = 22,
				SampleRate = 400
			};

			SaveItem(item);
		}
	}
}
