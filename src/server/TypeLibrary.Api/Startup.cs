using Mimirorg.Authentication.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TypeLibrary.Core.Extensions;

namespace TypeLibrary.Api
{
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
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetPreflightMaxAge(TimeSpan.FromSeconds(2520))
                        .Build());
            });

            // Add routing
            services.AddRouting(o => o.LowercaseUrls = true);

            // Add modules
            //services.AddApplicationInsightsLoggingModule();
            services.AddTypeLibraryModule(Configuration);
            services.AddMimirorgAuthenticationModule(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            });
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            if (!env.IsDevelopment())
                app.UseHttpsRedirection();


            app.UseCors("CorsPolicy");
            app.UseRouting();

            // User modules
            app.UseTypeLibraryModule();
            app.UseMimirorgAuthenticationModule();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}