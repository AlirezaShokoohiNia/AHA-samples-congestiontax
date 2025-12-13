using AHA.CongestionTax.Api;
using AHA.CongestionTax.Application.Extensions;
using AHA.CongestionTax.Infrastructure.Extensions;
using AHA.CongestionTax.Infrastructure.Query.Source2.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Application services
builder.Services.AddApplication();

// Infrastructure: write side (AppDbContext)
builder.Services.AddInfrastructureData(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("WriteDbConnection")));

// Infrastructure: read side (QueryDbContext)
builder.Services.AddInfrastructureSource1(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("ReadDbConnection")));

// Infrastructure: JSON/file sources
builder.Services.AddInfrastructureSource2();

//  Infrastructure: Options
builder.Services.Configure<RuleSetOptions>(
    builder.Configuration.GetSection("RuleSet"));

// Swagger
builder.Services.AddEndpointsApiExplorer();

#region Swagger Configuration

builder.Services.AddSwaggerGen(options =>
{
    var baseDir = AppContext.BaseDirectory;

    var xmlFiles = new[]
    {
            "Api.xml",
            "Application.xml",
    };

    foreach (var xmlFile in xmlFiles)
    {
        var xmlPath = Path.Combine(baseDir, xmlFile);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Congestion Tax API",
        Version = "v1",
        Description = "CQRS-based API for congestion tax calculation"
    });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    #region Swagger Configuration

    _ = app.UseSwagger();
    _ = app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Congestion Tax API v1");
        options.RoutePrefix = string.Empty; // Swagger UI at root "/"
    });

    #endregion
}

app.UseHttpsRedirection();

#region Map CQRS endpoints
// Application services
builder.Services.AddApplication();

// Infrastructure: write side (AppDbContext)
builder.Services.AddInfrastructureData(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("WriteDbConnection")));

// Infrastructure: read side (QueryDbContext)
builder.Services.AddInfrastructureSource1(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("ReadDbConnection")));

// Infrastructure: JSON/file sources
builder.Services.AddInfrastructureSource2();

// Options
builder.Services.Configure<RuleSetOptions>(
    builder.Configuration.GetSection("RuleSet"));

#endregion

app.Run();

// Make the implicit Program class public for integration tests
public partial class Program { }