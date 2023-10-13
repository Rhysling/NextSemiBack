namespace NextSemiBack.Mailer
{
	public class MailMessage : MailMessageBase
	{
		private static readonly List<string> levelColors = new()
		{
			"#337ab7", //None = 0,
			"#5bc0de", //Info = 1,
			"#f0ad4e", //Warn = 2,
			"#d9534f", //Error = 3,
			"#d953ad"  //Fatal = 4
		};

		public MailMessage(Models.ContactMessage model, string toEmails, bool isHtml = true) : base(isHtml)
		{
			base.toEmails = toEmails;

			subject = $"NEXT Contact from {model.Name ?? "unknown"}";
			body = """
				<html>
				<head>
					<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
					<meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=yes" />
					<title>[Title]</title>
				</head>
				<body style="margin: 0; padding: 0;">
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td width="10%" align="center" valign="top" style="background-color:#ffffff;">&nbsp;</td>
							<td width="80%" align="center" valign="top" style="background-color:#ffffff;">
								<!-- Content-->
								<table align="center" border="0" cellpadding="0" cellspacing="0" style="width:100%;
									max-width:800px;
									border:1px solid #dddddd;
									border-collapse:collapse;
									font-family:'Helvetica Neue',Helvetica,Arial,sans-serif;
									font-size:12px;
									background-color:#ffffff;
									color:#333333;">
									<tr>
										<td colspan="2" bgcolor="#000066" height="5" style="background-color:#000066;font-size:0;line-height:0">&nbsp;</td>
									</tr>
									<tr>
										<td colspan="2" align="center" bgcolor="[LogLevelColor]" style="background-color:[LogLevelColor]; color:#ffffff; padding:5px 0 5px 0; font-weight:bold;">
											[Title]
										</td>
									</tr>

									<tr>
										<td colspan="2" bgcolor="#dddddd" height="2" style="background-color:#dddddd;font-size:0;line-height:0">&nbsp;</td>
									</tr>

									<tr>
										<td align="right" style="padding: 0 5px 10px 5px;">Contact Name</td>
										<td align="left" style="padding: 0 5px 10px 5px;">[ContactName]</td>
									</tr>
									<tr>
										<td align="right" style="padding: 0 5px 10px 5px;">Email</td>
										<td align="left" style="padding: 0 5px 10px 5px;">[ContactEmail]</td>
									</tr>
									<tr>
										<td align="right" style="padding: 0 5px 10px 5px;">Company</td>
										<td align="left" style="padding: 0 5px 10px 5px;">[ContactCompany]</td>
									</tr>
									<tr>
										<td align="right" style="padding: 0 5px 10px 5px;">Phone</td>
										<td align="left" style="padding: 0 5px 10px 5px;">[ContactPhone]</td>
									</tr>
									<tr>
										<td align="right" style="padding: 0 5px 10px 5px;">Message</td>
										<td align="left" style="padding: 0 5px 10px 5px;">[ContactMessage]</td>
									</tr>

									<tr>
										<td colspan="2" bgcolor="#000066" height="2" style="background-color:#000066;font-size:0;line-height:0">&nbsp;</td>
									</tr>

									<tr>
										<td colspan="2" align="center" bgcolor="#ffffff" style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif;
											color: #333333; background-color:#ffffff;
											font-size: 11px;
											padding-top: 4px;
											padding-right: 4px;
											padding-bottom: 4px;
											padding-left: 4px;
											text-align:center;">
											[DateNow] - [TimeNow]
										</td>
									</tr>
								</table>

								<!-- End Content-->

							</td>
							<td width="10%" align="center" valign="top" style="background-color:#ffffff;">&nbsp;</td>
						</tr>
					</table>
				</body>
				</html>
				""";

			mergeFields.Add("Title", subject);
			//mergeFields.Add("LogDateGmt", model.EventDateGmt);
			//mergeFields.Add("LogLevelName", model.LevelName ?? "Missing");
			mergeFields.Add("LogLevelColor", levelColors[1]);

			mergeFields.Add("ContactName", string.IsNullOrWhiteSpace(model.Name) ? "None" : model.Name);
			mergeFields.Add("ContactEmail", string.IsNullOrWhiteSpace(model.Email) ? "None" : model.Email);
			mergeFields.Add("ContactCompany", string.IsNullOrWhiteSpace(model.Company) ? "None" : model.Company);
			mergeFields.Add("ContactPhone", string.IsNullOrWhiteSpace(model.Phone) ? "None" : model.Phone);
			mergeFields.Add("ContactMessage", string.IsNullOrWhiteSpace(model.Message) ? "None" : model.Message);

		}

	}
}
