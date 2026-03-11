using BarberLegacy.Api.Data;
using BarberLegacy.Api.Repositories;
using BarberLegacy.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var licenseKey = builder.Configuration["AutoMapper:LicenseKey"];

builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = licenseKey;
}, typeof(Program).Assembly);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("name=DefaultConnection"));

builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceService, ServiceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
