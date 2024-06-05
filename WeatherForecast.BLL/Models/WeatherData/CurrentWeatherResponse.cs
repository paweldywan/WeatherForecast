namespace WeatherForecast.BLL.Models.WeatherData
{
    public class CurrentWeatherResponse
    {
        public required Coord Coord { get; set; }

        public required Weather[] Weather { get; set; }

        public required string Base { get; set; }

        public required SimpleMain Main { get; set; }

        public int Visibility { get; set; }

        public required Wind Wind { get; set; }

        public Precipitation? Rain { get; set; }

        public Precipitation? Snow { get; set; }

        public required Clouds Clouds { get; set; }

        public int Dt { get; set; }

        public required Sys Sys { get; set; }

        public int Timezone { get; set; }

        public int Id { get; set; }

        public required string Name { get; set; }

        public int Cod { get; set; }
    }
}
