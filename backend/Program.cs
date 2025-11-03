using PnW.Query;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowReactApp");

app.MapGet("/", () => "Hello World!");

app.MapGet("/weatherforecast", () =>
{
    var forecasts = new[]
    {
        new { date = DateTime.Now, temperatureC = 23, summary = "Sunny" },
        new { date = DateTime.Now.AddDays(1), temperatureC = 19, summary = "Cloudy" },
        new { date = DateTime.Now.AddDays(2), temperatureC = 17, summary = "Rainy" }
    };

    return Results.Json(forecasts);
});

app.Run();