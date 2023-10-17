using Newtonsoft.Json;
using NextSemiBack.Models;

namespace NextSemiBack.Services;

public class IpItemDb
{
	private List<IpItem> db;
	private readonly string filePath;

	public IpItemDb(AppSettings aps)
	{
		string dir = Directory.GetCurrentDirectory();
		//D:\UserData\Documents\AppDev\NextSemiBack\NextSemiBack
		if (aps.NextSemi.IsProduction)
			filePath = Path.Combine(dir, @"wwwroot\docs\IpItemDb.json");
		else
			filePath = dir.Replace(@"NextSemiBack\NextSemiBack", @"NextSemiFront\public\docs\IpItemDb.json");

		if (File.Exists(filePath)) { 
			string json = File.ReadAllText(filePath);
			db = JsonConvert.DeserializeObject<List<IpItem>>(json) ?? new List<IpItem>();
		}
		else
			db = new List<IpItem>();

	}
	public string FilePath => filePath;

	public List<IpItem> Items => db;	

	public void SaveItem(IpItem item)
	{
		db = db.Where(a => a.Id != item.Id).ToList();

		if (item.Id == 0)
			item.Id = db.Max(a => a.Id) + 1;

		db.Add(item);
		File.WriteAllText(FilePath, JsonConvert.SerializeObject(db));
	}

	public void DeleteItem(IpItem item)
	{
		db = db.Where(a => a.Id != item.Id).ToList();
		File.WriteAllText(FilePath, JsonConvert.SerializeObject(db));
	}

	public void DeleteItem(int id)
	{
		db = db.Where(a => a.Id != id).ToList();
		File.WriteAllText(FilePath, JsonConvert.SerializeObject(db));
	}
}
