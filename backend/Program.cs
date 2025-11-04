using PnW.Query;
using PnW.Classes;
using DotNetEnv;

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

Env.Load();

string? apiKey = Environment.GetEnvironmentVariable("PNW_API_KEY");

if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("ERROR: PNW_API_KEY environment variable is not set. Cannot run the application.");
    return;
}

const string targetCityId = "1332734";
Dictionary<string, int?> mil = null;

try
{
    Console.WriteLine($"Fetching data for City ID: {targetCityId}...");

    var api = new API(apiKey);
    City data = await api.GetQuery<City>(targetCityId, ["name", "barracks", "factory", "hangar", "drydock"]);
    if (data != null)
    {
        mil = data.GetMilitary();
    }
    else
    {
        Console.WriteLine("Failed to retrieve city data or City object was null.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

app.MapGet("/cityinfo", () => Results.Json(mil));

app.Run();
