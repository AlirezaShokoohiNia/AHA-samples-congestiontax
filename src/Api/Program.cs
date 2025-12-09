var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
///...

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Congestion Tax API v1");
        options.RoutePrefix = string.Empty; // Swagger UI at root "/"
    });
}

app.UseHttpsRedirection();

// Map CQRS endpoints
//...

app.Run();
