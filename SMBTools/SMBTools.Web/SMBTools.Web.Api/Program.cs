using NLog.Web;
using SMBTools.Web.Api.Infrastructure;

#region setup
var builder = WebApplication.CreateBuilder(args);
var provider = builder.Services.BuildServiceProvider();
var webHostEnvironment = provider.GetRequiredService<IWebHostEnvironment>();

builder.Services.InitServices(builder.Configuration, webHostEnvironment);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

var app = builder.Build();
app.InitApp();
app.Run();
#endregion
