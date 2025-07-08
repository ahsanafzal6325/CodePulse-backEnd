using CodePulse.Application.Categories;
using CodePulse.Application.Common.Mapping;
using CodePulse.Domain.Repositories;
using CodePulse.EntityFrameworkCore.Data;
using CodePulse.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ?? Load Serilog config
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("serilog.json", optional: false, reloadOnChange: true);
});

// ? Register Serilog BEFORE app.Build()
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("Logs/startup-log.txt", rollingInterval: RollingInterval.Day);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ? App is built AFTER Serilog is configured
var app = builder.Build();

// Enable Swagger if in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ? Enable request logging
app.UseSerilogRequestLogging();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
