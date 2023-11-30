using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mimirorg.Authentication.Extensions;
using Mimirorg.Authentication.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tyle.Api;
using Tyle.Application;
using Tyle.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

// CORS policy
var corsOrigins = builder.Configuration.GetSection("CorsConfiguration").GetValue<string>("ValidOrigins")?.Split(",");
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policyBuilder =>
    {
        if (corsOrigins != null && corsOrigins.Any())
        {
            policyBuilder.WithOrigins(corsOrigins).AllowCredentials();
        }
        else
        {
            policyBuilder.AllowAnyOrigin();
        }

        policyBuilder.AllowAnyHeader().AllowAnyMethod().SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
    });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add authentication
builder.Services.AddMimirorgAuthenticationModule(builder.Configuration);

builder.Services
    .AddApplicationServices()
    .AddDatabaseConfiguration(builder.Configuration)
    .AddRequestToDomainMapping()
    .AddRepositories()
    .AddDomainToViewMapping()
    .AddApiServices();

// Swagger configurations
var swaggerConfigurationSection = builder.Configuration.GetSection(nameof(SwaggerConfiguration));
var swaggerConfiguration = new SwaggerConfiguration();
swaggerConfigurationSection.Bind(swaggerConfiguration);
builder.Services.Configure<SwaggerConfiguration>(swaggerConfigurationSection.Bind);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("tyle-api",
        new OpenApiInfo
        {
            Title = swaggerConfiguration.Title,
            Description = swaggerConfiguration.Description,
            Contact = new OpenApiContact { Name = swaggerConfiguration.Contact?.Name, Email = swaggerConfiguration.Contact?.Email }
        });

    var xmlPath = Path.Combine(AppContext.BaseDirectory, "swagger.xml");

    c.IncludeXmlComments(xmlPath, true);
    c.CustomSchemaIds(x => x.FullName);
    c.EnableAnnotations();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/tyle-api/swagger.json", "Tyle API");
        c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
        c.DisplayOperationId();
        c.DisplayRequestDuration();
        c.RoutePrefix = string.Empty;
    });

    app.UseDeveloperExceptionPage();
}

if (!builder.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("CorsPolicy");

app.UseMimirorgAuthenticationModule();
app.UseCookiePolicy();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TyleDbContext>();    
    if (context.Database.IsRelational())
    {
        context.Database.Migrate();
    }     
}

if (builder.Configuration.GetValue<bool>("FetchDataFromCL"))
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<TyleDbContext>();
        var autoMapperService = (IMapper) services.GetService(typeof(IMapper));
        var savingDataService = new Tyle.External.SupplyExternalData(context, autoMapperService);
        var responseAddingDataToDb = await savingDataService.SupplyData();
    }
}

app.MapControllers();

app.Run();

namespace Tyle.Api
{
    public partial class Program { }
} // so you can reference it from tests