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
const string targetCityId2 = "1332849";
Dictionary<string, int?> mil1 = null;
Dictionary<string, int?> mil2 = null;
string name1 = null;
string name2 = null;

try
{
    Console.WriteLine($"Fetching data for City ID: {targetCityId}...");

    var api = new API(apiKey);
    City city1 = await api.GetQuery<City>(targetCityId, ["name", "barracks", "factory", "hangar", "drydock"]);
    name1 = city1.name;
    mil1 = city1.GetMilitary();
    City city2 = await api.GetQuery<City>(targetCityId2, ["name", "barracks", "factory", "hangar", "drydock"]);
    name2 = city2.name;
    mil2 = city2.GetMilitary();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

app.MapGet("/cityinfo", () => new { name1 = name1, mil1 = mil1, name2 = name2, mil2 = mil2 });

app.Run();
