using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Services;

namespace WeatherForecast.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddHttpClient();

            services.AddHttpClient<IWeatherService, WeatherService>(o => o.BaseAddress = new Uri($"https://api.openweathermap.org/data/3.0/onecall"));

            services.AddHttpClient<IGeocodingService, GeocodingService>(o => o.BaseAddress = new Uri("http://api.openweathermap.org/geo/1.0"));

            var app = builder.Build();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
