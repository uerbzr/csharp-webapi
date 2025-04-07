using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using workshop.webapi.Data;
using workshop.webapi.Endpoints;
using workshop.webapi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("WeatherDB"));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Demo API");
    });
    app.MapScalarApiReference();
}
app.UseHttpsRedirection();

app.ConfigureWeatherEndpoints();
app.ConfigureSeedEndpoints();

app.Run();

