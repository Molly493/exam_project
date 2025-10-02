using Microsoft.AspNetCore.Mvc;
using WebApiExamen.Models;
using WebApiExamen.Services;

namespace WebApiExamen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        public WeatherForecastController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherService.GetForecasts();
        }
    }
}
