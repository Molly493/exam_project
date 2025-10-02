using WebApiExamen.Models;

namespace WebApiExamen.Services
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetForecasts();
    }
}
