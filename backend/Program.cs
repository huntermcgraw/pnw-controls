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

Env.Load();
string? apiKey = Environment.GetEnvironmentVariable("PNW_API_KEY");
if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("ERROR: PNW_API_KEY is not set.");
    return;
}

app.MapPost("/cityinfo", async (ComparisonRequest request) =>
{
    if (string.IsNullOrEmpty(request.cityID1) || string.IsNullOrEmpty(request.cityID2))
    {
        return Results.BadRequest(new { message = "Try again." });
    }

    string targetCityId = request.cityID1; // test with 1332734
    string targetCityId2 = request.cityID2; // test with 1332849

    try
    {
        var api = new API(apiKey);

        Task<City> city1Task = api.GetQuery<City>(targetCityId, ["name", "barracks", "factory", "hangar", "drydock"]);
        Task<City> city2Task = api.GetQuery<City>(targetCityId2, ["name", "barracks", "factory", "hangar", "drydock"]);

        await Task.WhenAll(city1Task, city2Task);

        City city1 = city1Task.Result;
        City city2 = city2Task.Result;
        
        Dictionary<string, int?> mil1 = city1.GetMilitary();
        Dictionary<string, int?> mil2 = city2.GetMilitary();

        return Results.Ok(new
        {
            name1 = city1.name,
            name2 = city2.name,
            mil1 = mil1,
            mil2 = mil2,
        });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
        return Results.Json(new { message = "An error occurred." }, statusCode: 500);
    }
    
});

app.Run();
