using Newtonsoft.Json;
using System.Net;

namespace NextSemiBack.Recaptcha
{
	public class RecaptchaService
	{
		private readonly HttpClient client;
		private const string verifyAddress = "https://www.google.com/recaptcha/api/siteverify";
		private const string secret = "6Lf7mJsoAAAAAObUnIG9GTctgCboKILOKMGE8iSf";

		public RecaptchaService()
		{
			var socketsHandler = new SocketsHttpHandler
			{
					PooledConnectionLifetime = TimeSpan.FromMinutes(10),
					PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
					MaxConnectionsPerServer = 10
			};
			client = new HttpClient(socketsHandler)
			{
					BaseAddress = new Uri(verifyAddress)
			};
		}

		public async Task<RecaptchaVerificationResult> VerifyAsync(RecaptchaToken token)
		{
			var parameters = new Dictionary<string, string> {
				{
						"secret", secret
				},
				{
						"response", token.Token
				}
			};
			var encodedContent = new FormUrlEncodedContent(parameters);

			HttpResponseMessage res = await client.PostAsync("", encodedContent).ConfigureAwait(false);

			if (res.StatusCode == HttpStatusCode.OK)
			{
				string json = await res.Content.ReadAsStringAsync();
				var rvr = JsonConvert.DeserializeObject<RecaptchaVerificationResult>(json);
				string[] tp1 = { "Deserialization of verification result failed." };

				return rvr ?? new RecaptchaVerificationResult { ErrorCodes =  tp1};
			}

			string[] tp = {
				"Verification request failed",
				$"Status Code: {res.StatusCode}",
				$"Reason {res.ReasonPhrase}",
				$"Message: {res.RequestMessage}" };

			return new RecaptchaVerificationResult { ErrorCodes = tp }; ;
		}

	}
}

