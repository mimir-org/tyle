using Mimirorg.Authentication.Extensions;
using Mimirorg.Common;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Middleware;
using Mimirorg.Common.Models;
using Newtonsoft.Json;
using TypeLibrary.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole().AddConsoleFormatter<CustomTimePrefixingFormatter, CustomWrappingConsoleFormatterOptions>();

builder.Configuration.AddJsonFile($"{Directory.GetCurrentDirectory()}/appsettings.local.json", true);

builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    o.SerializerSettings.ContractResolver = new CamelCaseExceptDictionaryKeysResolver();
});

// Add Cors policy
var origins = builder.Configuration.GetSection("CorsConfiguration").GetValue<string>("ValidOrigins")?.Split(",");
var hasOrigins = origins != null && origins.Any();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        if (hasOrigins)
        {
            builder.WithOrigins(origins!).AllowCredentials();
        }
        else
        {
            builder.AllowAnyOrigin();
        }

        builder.AllowAnyHeader().AllowAnyMethod().SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
    });
});

// Add routing
builder.Services.AddRouting(o => o.LowercaseUrls = true);

// Add modules
builder.Services.AddMimirorgAuthenticationModule();
builder.Services.AddTypeLibraryModule(builder.Configuration);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

if (!builder.Environment.IsDevelopment())
    app.UseHttpsRedirection();


app.UseCors("CorsPolicy");
app.UseRouting();
app.UseDynamicImageMiddleware();

// User modules
app.UseTypeLibraryModule();
app.UseMimirorgAuthenticationModule();
app.UseCookiePolicy();

app.MapControllers();

app.Run();

public partial class Program { } // so you can reference it from tests
