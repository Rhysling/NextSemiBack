﻿using Newtonsoft.Json;

namespace NextSemiBack.Recaptcha;

public class RecaptchaVerificationResult
{
	public bool Success { get; set; }
	public double Score { get; set; }
	public string Action { get; set; } = "";
	public string Challenge_ts { get; set; } = "";
	public string Hostname { get; set; } = "";

	[JsonProperty("error-codes")]
	public string[] ErrorCodes { get; set; } = { "" };
}


//	"success": true | false,    // whether this request was a valid reCAPTCHA token for your site
//	"score": number             // the score for this request (0.0 - 1.0)
//	"action": string            // the action name for this request (important to verify)
//	"challenge_ts": timestamp,  // timestamp of the challenge load (ISO format yyyy-MM-dd'T'HH:mm:ssZZ)
//	"hostname": string,         // the hostname of the site where the reCAPTCHA was solved
//	"error-codes": [...]        // optional