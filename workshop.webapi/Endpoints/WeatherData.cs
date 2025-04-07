using workshop.webapi.Data;
using workshop.webapi.Models;

namespace workshop.webapi.Endpoints
{
    public static class WeatherData
    {
        public static void ConfigureSeedEndpoints(this WebApplication app)
        {
            app.MapGet("/seed", async (DataContext db) =>
            {
                if (!db.WeatherForecasts.Any())
                {
                    db.WeatherForecasts.AddRange(new List<WeatherForecast>
                    {
                        new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 20, Summary = "Warm" },
                        new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), TemperatureC = 25, Summary = "Hot" },
                        new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)), TemperatureC = 15, Summary = "Cool" }
                    });
                    await db.SaveChangesAsync();
                    
                    return Results.Ok("Database had data");
                }
                else
                {
                    return Results.Ok("Database seeded");
                }
            });
        }
    }
}
