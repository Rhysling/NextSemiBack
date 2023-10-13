namespace NextSemiBack.Mailer
{
	public class MailMessageBase
	{
		protected string? toEmails; // ',' delimited
		protected string? subject;
		protected bool isHtml = true;
		protected string? body;
		protected Dictionary<string, string> mergeFields;

		public MailMessageBase(bool isHtml = true)
		{
			this.isHtml = isHtml;
			TimeZoneInfo pstZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
			DateTime pstNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, pstZone);

			mergeFields = new Dictionary<string, string> {
				{"DateNow", pstNow.ToShortDateString()},
				{"TimeNow", pstNow.ToLongTimeString() + (pstZone.IsDaylightSavingTime(pstNow) ? " PDT" : " PST")}
			};
		}

		public string ToEmails
		{
			get
			{
				if (String.IsNullOrWhiteSpace(toEmails))
					throw new ArgumentNullException("ToEmails cannot be empty.");

				return toEmails;
			}
		}

		public string Subject
		{
			get
			{
				if (String.IsNullOrWhiteSpace(subject))
					throw new ArgumentNullException("Subject cannot be empty.");

				return subject;
			}
		}

		public bool IsHtml
		{
			get
			{
				return isHtml;
			}
		}

		public string RebderBody()
		{
			if (String.IsNullOrWhiteSpace(body))
				throw new ArgumentNullException("Body cannot be empty.");

			foreach (var r in mergeFields)
				body = body.Replace("[" + r.Key + "]", r.Value);

			return body;
		}

	}
}