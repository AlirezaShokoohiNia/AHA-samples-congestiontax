using AHA.CongestionTax.Application.Extensions;
using AHA.CongestionTax.Infrastructure.Extensions;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Register Application + Infrastructure
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

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

// Map CQRS endpoints
ApiEndpoints.Map(app);

app.Run();
