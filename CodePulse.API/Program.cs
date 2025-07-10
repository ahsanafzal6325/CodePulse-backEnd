using CodePulse.Application.Categories;
using CodePulse.Application.Common.Mapping;
using CodePulse.Domain.Repositories;
using CodePulse.EntityFrameworkCore.Data;
using CodePulse.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Load additional config (e.g. serilog.json)
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("serilog.json", optional: true, reloadOnChange: true);
});

// Configure Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File(
            "Logs/startup-log.txt",
            rollOnFileSizeLimit: true,
            fileSizeLimitBytes: 10_000_000, // 10 MB
            retainedFileCountLimit: null,   // Keep all old rolled files
            shared: true                    // Safe for multi-process
        );
});

// Add services
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

// Build the app
var app = builder.Build();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
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
