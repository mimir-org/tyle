using Mimirorg.Authentication.Extensions;
using Mimirorg.Common;
using Mimirorg.Common.Middleware;
using Newtonsoft.Json;
using TypeLibrary.Core.Extensions;

namespace TypeLibrary.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            o.SerializerSettings.ContractResolver = new CamelCaseExceptDictionaryKeysResolver();
        });

        // Add Cors policy
        var origins = Configuration.GetSection("CorsConfiguration").GetValue<string>("ValidOrigins")?.Split(",");
        var hasOrigins = origins != null && origins.Any();

        services.AddCors(options =>
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
        services.AddRouting(o => o.LowercaseUrls = true);

        // Add modules
        services.AddMimirorgAuthenticationModule();
        services.AddTypeLibraryModule(Configuration);
    }

    public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        if (!env.IsDevelopment())
            app.UseHttpsRedirection();


        app.UseCors("CorsPolicy");
        app.UseRouting();
        app.UseDynamicImageMiddleware();

        // User modules
        app.UseTypeLibraryModule();
        app.UseMimirorgAuthenticationModule();
        app.UseCookiePolicy();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}