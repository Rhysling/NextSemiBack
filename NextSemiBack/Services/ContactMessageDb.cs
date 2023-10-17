using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using NextSemiBack.Models;

namespace NextSemiBack.Services
{
	public class ContactMessageDb
	{
		private readonly List<ContactMessage> db;
		private readonly string filePath;

		public ContactMessageDb(AppSettings aps)
		{
			string dir = Directory.GetCurrentDirectory();
			//D:\UserData\Documents\AppDev\NextSemiBack\NextSemiBack
			if (aps.NextSemi.IsProduction)
				filePath = Path.Combine(dir, @"wwwroot\docs\ContactMessageDb.json");
			else
				filePath = dir.Replace(@"NextSemiBack\NextSemiBack", @"NextSemiFront\public\docs\ContactMessageDb.json");

			if (File.Exists(filePath))
			{
				string json = File.ReadAllText(filePath);
				db = JsonConvert.DeserializeObject<List<ContactMessage>>(json) ?? new List<ContactMessage>();
			}
			else
				db = new List<ContactMessage>();

		}
		public string FilePath => filePath;

		public List<ContactMessage> Items => db;

		public void Add(ContactMessage item)
		{
			db.Add(item);
			File.WriteAllText(FilePath, JsonConvert.SerializeObject(db));
		}
	}
}
