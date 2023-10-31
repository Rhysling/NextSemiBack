using Newtonsoft.Json;
using NextSemiBack.Models;

namespace NextSemiBack.Services;

public class UsersDb
{
	private readonly List<UserClient> db;
	private readonly string filePath;

	public UsersDb(AppSettings aps)
	{
		string dir = Directory.GetCurrentDirectory();
		//D:\UserData\Documents\AppDev\NextSemiBack\NextSemiBack
		if (aps.NextSemi.IsProduction)
			filePath = Path.Combine(dir, @"wwwroot\docs\UsersDb.json");
		else
			filePath = dir.Replace(@"NextSemiBack\NextSemiBack", @"NextSemiFront\public\docs\UsersDb.json");

		if (File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);
			db = JsonConvert.DeserializeObject<List<UserClient>>(json) ?? new List<UserClient>();
		}
		else
			db = new List<UserClient>();

	}


	// Properties
	public string FilePath => filePath;


	// Methods

	public List<UserClientRemote> GetAll()
	{
		return db.Select(u => new UserClientRemote {
			UserId = u.UserId,
			Email = u.Email,
			FullName = u.FullName,
			Token = u.Token,
			IsAdmin = u.IsAdmin,
			IsDisabled = u.IsDisabled
		}).ToList();
	}

	public UserClientRemote? FindByEmail(string email)
	{
		var u = db.FirstOrDefault(a => a.Email == email.ToLower());

		if (u is null)
			return null;

		return new UserClientRemote
		{
			UserId = u.UserId,
			Email = u.Email,
			FullName = u.FullName,
			Token = u.Token,
			IsAdmin = u.IsAdmin,
			IsDisabled = u.IsDisabled
		};
	}

	public UserClient? FindUcByEmail(string email)
	{
		return db.FirstOrDefault(a => a.Email == email.ToLower());
	}

	public UserClientRemote SaveUser(UserClientRemote ucr)
	{
		var u = FindUcByEmail(ucr.Email);

		if (u is not null)
		{
			//u.UserId
			//u.Email
			//u.PwHash = user.PwHash;
			u.FullName = ucr.FullName;
			u.Token = ucr.Token;
			u.IsAdmin = ucr.IsAdmin;
			u.IsDisabled = ucr.IsDisabled;
		}
		else
		{
			ucr.UserId = db.Any() ? db.Max(a => a.UserId) + 1 : 1;
			ucr.Email = ucr.Email.ToLower();

			var uc = new UserClient {
				UserId = ucr.UserId,
				Email = ucr.Email,
				PwHash = null,
				FullName = ucr.FullName,
				Token = ucr.Token,
				IsAdmin = ucr.IsAdmin,
				IsDisabled = ucr.IsDisabled
			};

			db.Add(uc);
		}

		File.WriteAllText(FilePath, JsonConvert.SerializeObject(db));
		return ucr;
	}

	public void SavePassword(string email, string pw)
	{
		var u = FindUcByEmail(email);

		if (u is not null)
		{
			u.PwHash = BCrypt.Net.BCrypt.EnhancedHashPassword(pw, 13);
			File.WriteAllText(FilePath, JsonConvert.SerializeObject(db));
		}
	}

	public void SaveToken(string email, string token)
	{
		var uc = FindUcByEmail(email);

		if (uc is not null)
		{
			uc.Token = token;
			File.WriteAllText(FilePath, JsonConvert.SerializeObject(db));
		}
	}

	public bool ValidatePassword(string email, string pw)
	{
		var u = FindUcByEmail(email);

		if (u is null || u.PwHash is null)
			return false;

		return BCrypt.Net.BCrypt.EnhancedVerify(pw, u.PwHash);
	}

}