using NextSemiBack.Models;

namespace NextSemiBack.Mailer
{
	public class MailgunTarget
	{
		private readonly HttpClient client;
		private const string baseAddress = "https://api.mailgun.net/v3/{0}/messages";
		private const string fromAddress = "noreply@nextsemi-demo.com";

		public MailgunTarget(AppSettings aps)
		{
			var socketsHandler = new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(10),
				PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
				MaxConnectionsPerServer = 10
			};
			client = new HttpClient(socketsHandler)
			{
				BaseAddress = new Uri(String.Format(baseAddress, aps.Mailgun.FromDomain))
			};
			client.DefaultRequestHeaders.Add("Authorization", aps.Mailgun.AuthValue);
		}

		public async Task<HttpResponseMessage> SendAsync(ContactMessage item, string toAddresses)
		{

			var msg = new MailMessage(item, toAddresses);

			var parameters = new Dictionary<string, string> {
				{ "from", fromAddress },
				{ "to", toAddresses },
				{ "subject", msg.Subject },
				{ "text", $"Contact name: {item.Name};\r\n Email: {item.Email};\r\n Company: {item.Company};\r\n Phone: {(string.IsNullOrWhiteSpace(item.Phone) ? "None" : item.Message)};\r\n Message: {(string.IsNullOrWhiteSpace(item.Message) ? "None" : item.Message)}" },
				{ "html", msg.RebderBody() }
			};

			var encodedContent = new FormUrlEncodedContent(parameters);

			HttpResponseMessage res = await client.PostAsync("", encodedContent).ConfigureAwait(false);

			return res;
		}

	}
}