using Microsoft.AspNetCore.StaticFiles;
using NextSemiBack.Mailer;
using NextSemiBack.Models;
using NextSemiBack.Recaptcha;
//using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<AppSettings>(builder.Configuration);

var aps = builder.Configuration.Get<AppSettings>();

if (aps != null)
{
	builder.Services.AddSingleton(aps);
}

builder.Services.AddSingleton<MailgunService>();
builder.Services.AddSingleton<RecaptchaService>();

//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication(options =>
//{
//	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//	options.Authority = builder.Configuration["Feeder:AuthDomain"];
//	options.Audience = builder.Configuration["Feeder:AuthAudience"];
//});

var app = builder.Build();


app.UseHttpsRedirection();


app.Use(async (context, next) =>
{
	await next();

	var path = context.Request.Path.Value ?? "/";
	bool notApi = !path.StartsWith("/api");
	bool noExt = !Path.HasExtension(path);

	if (notApi && noExt)
	{
		context.Request.Path = "/index.html";
		await next();
	}
});


var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".html"] = "text/html";
provider.Mappings[".webmanifest"] = "application/manifest+json";

app.UseStaticFiles(new StaticFileOptions()
{
	ContentTypeProvider = provider
});

if (app.Environment.IsDevelopment())
	app.UseCors(builder =>
	{
		builder
		//.AllowAnyOrigin()
		.WithOrigins("http://localhost:5050")
		//.AllowAnyMethod()
		.WithMethods("POST", "GET", "OPTIONS", "PUT")
		.AllowAnyHeader()
		.AllowCredentials();
	});

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
