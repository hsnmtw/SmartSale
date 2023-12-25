
using Microsoft.AspNetCore.Builder;
using Serilog;


using System.Globalization;
using System.Reflection;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Routing;

var port = 80;

var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Directory.GetCurrentDirectory();
var configuration = new ConfigurationBuilder()
        .SetBasePath(basePath)
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
        .Build();


var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

var builder = WebApplication.CreateBuilder(new WebApplicationOptions(){
    Args = args,
    EnvironmentName = "Production",
    ContentRootPath = basePath,
    ApplicationName = "SmartSales",
    WebRootPath = Path.Combine(basePath,"public")
});

builder.Host.UseSerilog();

builder.WebHost.UseKestrel(op => op.ListenAnyIP(port));

builder.Services.AddMediatR(op => op.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

// app.UseAuthorization();
// app.UseAuthentication();
// app.UseSession();
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();



logger.Information("  ░██████╗███╗░░░███╗░█████╗░██████╗░████████╗░██████╗░█████╗░██╗░░░░░███████╗");
logger.Information("  ██╔════╝████╗░████║██╔══██╗██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║░░░░░██╔════╝");
logger.Information("  ╚█████╗░██╔████╔██║███████║██████╔╝░░░██║░░░╚█████╗░███████║██║░░░░░█████╗░░");
logger.Information("  ░╚═══██╗██║╚██╔╝██║██╔══██║██╔══██╗░░░██║░░░░╚═══██╗██╔══██║██║░░░░░██╔══╝░░");
logger.Information("  ██████╔╝██║░╚═╝░██║██║░░██║██║░░██║░░░██║░░░██████╔╝██║░░██║███████╗███████╗");
logger.Information("  ╚═════╝░╚═╝░░░░░╚═╝╚═╝░░╚═╝╚═╝░░╚═╝░░░╚═╝░░░╚═════╝░╚═╝░░╚═╝╚══════╝╚══════╝");
logger.Information("  +==========================================================================+");
logger.Information(" (                BY HUSSAIN ABDULLAH ALMUTAWA, 0508456745                    )");
logger.Information("  +==========================================================================+");
logger.Information("Listening on Port : {port}", port);

app.Run();