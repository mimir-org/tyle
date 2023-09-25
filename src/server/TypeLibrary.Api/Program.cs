using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TypeLibrary.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services
    .AddDatabaseConfiguration(builder.Configuration)
    .AddRequestToDomainMapping()
    .AddRepositories();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!builder.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TyleDbContext>();
    context.Database.Migrate();
}

app.MapControllers();

app.Run();