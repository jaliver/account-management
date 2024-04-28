using CustomerService.Api.Data;
using CustomerService.Api.Repositories;
using CustomerService.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService.Api.Services.CustomerService>();

builder.Services.AddDbContext<CustomerDbContext>(options => 
    options.UseSqlite(CreateConfiguration().GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Services.SaveSwaggerJson();

app.Run();

static IConfiguration CreateConfiguration() =>
    new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

public static class SwaggerExtensions
{
    public static void SaveSwaggerJson(this IServiceProvider provider)
    {
        var sw = provider.GetRequiredService<ISwaggerProvider>();
        var doc = sw.GetSwagger("v1", null, "/");
        var swaggerFile = doc.SerializeAsJson(Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0);
        File.WriteAllText("openapi.json", swaggerFile);
    }
}