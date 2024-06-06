namespace WeatherForecast.BLL.Interfaces
{
    public interface ICoordinatesRequest
    {
        float Lat { get; set; }

        float Lon { get; set; }
    }
}
