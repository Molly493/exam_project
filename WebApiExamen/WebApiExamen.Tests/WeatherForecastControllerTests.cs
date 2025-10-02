using Microsoft.AspNetCore.Mvc;
using WebApiExamen.Controllers;
using WebApiExamen.Models;
using WebApiExamen.Services;
using Moq;
using Xunit;

namespace WebApiExamen.WebApiExamen.Tests;
public class WeatherForecastControllerTests
{
    private readonly Mock<IWeatherService> _mockWeatherService;
    private readonly WeatherForecastController _controller;

    public WeatherForecastControllerTests()
    {
        _mockWeatherService = new Mock<IWeatherService>();
        _controller = new WeatherForecastController(_mockWeatherService.Object);
    }

    [Fact]
    public void Get_ReturnsActionResultWithForecasts()
    {
        // Arrange: Configura el mock para retornar datos falsos
        var mockForecasts = new List<WeatherForecast>
        {
            new WeatherForecast {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 25, 
                Summary = "Sunny" }
        };
        _mockWeatherService.Setup(service => service.GetForecasts()).Returns(mockForecasts);

        // Act: Ejecuta el método
        var result = _controller.Get() as OkObjectResult;

        // Assert: Verifica el resultado
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var forecasts = Assert.IsType<List<WeatherForecast>>(result.Value);
        Assert.Single(forecasts);  // Espera exactamente 1 elemento
        Assert.Equal("Sunny", forecasts[0].Summary);
    }

    [Fact]
    public void Get_WhenServiceThrowsException_ReturnsBadRequest()
    {
        // Arrange: Mock que lanza excepción
        _mockWeatherService.Setup(service => service.GetForecasts()).Throws(new InvalidOperationException("Error en servicio"));

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _controller.Get());
    }
}
