using Microsoft.EntityFrameworkCore;
using workshop.webapi.Models;

namespace workshop.webapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    }
}
