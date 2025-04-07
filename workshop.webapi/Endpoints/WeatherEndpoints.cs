using Microsoft.EntityFrameworkCore;
using workshop.webapi.Data;
using workshop.webapi.Models;

namespace workshop.webapi.Endpoints
{
    public static class WeatherEndpoints
    {
        public static void ConfigureWeatherEndpoints(this WebApplication app)
        {
            var weather = app.MapGroup("/weather");

            weather.MapGet("/", async (DataContext db) =>
            {
                var entities = await db.WeatherForecasts.ToListAsync();
                return Results.Ok(entities);
            });

            weather.MapGet("/{id}", async (int id, DataContext db) =>
            {
                var entity = await db.WeatherForecasts.FindAsync(id);
                return entity is not null ? Results.Ok(entity) : Results.NotFound();
            }); 

            weather.MapDelete("/{id}", async (int id, DataContext db) =>
            {
                var entity = await db.WeatherForecasts.FindAsync(id);
                if (entity is null)
                {
                    return Results.NotFound();
                }
                db.WeatherForecasts.Remove(entity);
                await db.SaveChangesAsync();
                return Results.Ok(entity);
            });


            weather.MapPut("/{id}", async (int id, WeatherForecast model, DataContext db) =>
            {
                var target = await db.WeatherForecasts.FindAsync(id);
                if (target is null)
                {
                    return Results.NotFound();
                }
                target.TemperatureC = model.TemperatureC;
                target.Date = model.Date;
                target.Summary = model.Summary;
                db.Update(target);
                await db.SaveChangesAsync();
                return Results.Ok(target);
            });

            weather.MapPost("/", async (WeatherForecast model, DataContext db) =>
            {
                db.WeatherForecasts.Add(model);
                await db.SaveChangesAsync();
                return Results.Created($"/weather/{model.Id}", model);
            });
            
        }
    }
}
