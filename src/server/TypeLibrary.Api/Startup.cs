using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TypeLibrary.Core.Extensions;

namespace TypeLibrary.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        //private SwaggerConfiguration _swaggerConfiguration;

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
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Add routing
            services.AddRouting(o => o.LowercaseUrls = true);

            // Add Azure Active Directory Module and Swagger Module
            //var (swaggerConfiguration, activeDirectoryConfiguration) =
            //    services.AddAzureActiveDirectoryModule(Configuration);
            //_activeDirectoryConfiguration = activeDirectoryConfiguration;
            //_swaggerConfiguration = swaggerConfiguration;

            //services.AddApplicationInsightsLoggingModule();
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

            // Use Azure Active Directory Module and Swagger Module

            //app.UseAzureActiveDirectoryModule(_activeDirectoryConfiguration, _swaggerConfiguration);
            app.UseTypeLibraryModule();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}