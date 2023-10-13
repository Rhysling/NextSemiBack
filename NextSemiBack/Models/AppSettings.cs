namespace NextSemiBack.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public class AppSettings
{
	public ASNextSemi NextSemi { get; set; }

	public ASMailgun Mailgun { get; set; }
}


public class ASNextSemi
{
	public string IsProductionString { get; set; }
	public bool IsProduction => IsProductionString == "true";
	public string BaseUrl { get; set; }
	public string AuthDomain { get; set; }
	public string AuthAudience { get; set; }
	public string AuthClientId { get; set; }
}

public class ASMailgun
{
	public string FromDomain
	{
		get { return "nextsemi-demo.com"; }
		set { _ = value; }
	}

	public string AuthValue { get; set; }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
