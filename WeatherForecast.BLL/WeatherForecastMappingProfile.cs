using AutoMapper;
using WeatherForecast.BLL.Models;
using WeatherForecast.BLL.Models.WeatherData;

namespace WeatherForecast.BLL
{
    public class WeatherForecastMappingProfile : Profile
    {
        public WeatherForecastMappingProfile()
        {
            CreateMap<List, WeatherForecastModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.Dt).DateTime))
                .ForMember(dest => dest.TemperatureK, opt => opt.MapFrom(src => src.Main.Temp))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Weather.First().Description));
        }
    }
}
