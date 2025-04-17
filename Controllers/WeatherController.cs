using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers;

/// <summary>
/// Controller for accessing weather data from the Open Meteo API.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    private readonly ILogger<WeatherController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherController"/> class.
    /// </summary>
    /// <param name="weatherService">The service used to retrieve weather data.</param>
    /// <param name="logger">Logger for diagnostic information.</param>
    public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger)
    {
        _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets weather data for a specific location defined by coordinates.
    /// </summary>
    /// <param name="latitude">The latitude of the location (between -90 and 90).</param>
    /// <param name="longitude">The longitude of the location (between -180 and 180).</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>Weather data for the specified location.</returns>
    /// <response code="200">Returns the weather data for the specified location.</response>
    /// <response code="400">If the coordinates are invalid.</response>
    /// <response code="500">If there was an error processing the request.</response>
    [HttpGet]
    [ProducesResponseType(typeof(WeatherResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetWeather(
        [FromQuery] double latitude,
        [FromQuery] double longitude,
        CancellationToken cancellationToken)
    {
        // Validate coordinates
        if (latitude < -90 || latitude > 90)
        {
            _logger.LogWarning("Invalid latitude provided: {Latitude}", latitude);
            return BadRequest("Latitude must be between -90 and 90 degrees.");
        }

        if (longitude < -180 || longitude > 180)
        {
            _logger.LogWarning("Invalid longitude provided: {Longitude}", longitude);
            return BadRequest("Longitude must be between -180 and 180 degrees.");
        }

        try
        {
            var weatherData = await _weatherService.GetWeatherDataAsync(latitude, longitude, cancellationToken);
            return Ok(weatherData);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error occurred while fetching weather data from the API");
            return StatusCode(StatusCodes.Status503ServiceUnavailable, "Weather service is currently unavailable. Please try again later.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while processing the weather request");
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again later.");
        }
    }

    /// <summary>
    /// Gets the current weather data for a specific location defined by coordinates.
    /// </summary>
    /// <param name="latitude">The latitude of the location (between -90 and 90).</param>
    /// <param name="longitude">The longitude of the location (between -180 and 180).</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>Current weather data for the specified location.</returns>
    /// <response code="200">Returns the current weather data for the specified location.</response>
    /// <response code="400">If the coordinates are invalid.</response>
    /// <response code="500">If there was an error processing the request.</response>
    [HttpGet("current")]
    [ProducesResponseType(typeof(CurrentWeather), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCurrentWeather(
        [FromQuery] double latitude,
        [FromQuery] double longitude,
        CancellationToken cancellationToken)
    {
        // Validate coordinates
        if (latitude < -90 || latitude > 90)
        {
            _logger.LogWarning("Invalid latitude provided: {Latitude}", latitude);
            return BadRequest("Latitude must be between -90 and 90 degrees.");
        }

        if (longitude < -180 || longitude > 180)
        {
            _logger.LogWarning("Invalid longitude provided: {Longitude}", longitude);
            return BadRequest("Longitude must be between -180 and 180 degrees.");
        }

        try
        {
            var weatherData = await _weatherService.GetWeatherDataAsync(latitude, longitude, cancellationToken);
            return Ok(weatherData.Current);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error occurred while fetching current weather data from the API");
            return StatusCode(StatusCodes.Status503ServiceUnavailable, "Weather service is currently unavailable. Please try again later.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while processing the current weather request");
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again later.");
        }
    }

    /// <summary>
    /// Gets the hourly forecast data for a specific location defined by coordinates.
    /// </summary>
    /// <param name="latitude">The latitude of the location (between -90 and 90).</param>
    /// <param name="longitude">The longitude of the location (between -180 and 180).</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>Hourly forecast data for the specified location.</returns>
    /// <response code="200">Returns the hourly forecast data for the specified location.</response>
    /// <response code="400">If the coordinates are invalid.</response>
    /// <response code="500">If there was an error processing the request.</response>
    [HttpGet("hourly")]
    [ProducesResponseType(typeof(HourlyForecast), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHourlyForecast(
        [FromQuery] double latitude,
        [FromQuery] double longitude,
        CancellationToken cancellationToken)
    {
        // Validate coordinates
        if (latitude < -90 || latitude > 90)
        {
            _logger.LogWarning("Invalid latitude provided: {Latitude}", latitude);
            return BadRequest("Latitude must be between -90 and 90 degrees.");
        }

        if (longitude < -180 || longitude > 180)
        {
            _logger.LogWarning("Invalid longitude provided: {Longitude}", longitude);
            return BadRequest("Longitude must be between -180 and 180 degrees.");
        }

        try
        {
            var weatherData = await _weatherService.GetWeatherDataAsync(latitude, longitude, cancellationToken);
            return Ok(weatherData.Hourly);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error occurred while fetching hourly forecast data from the API");
            return StatusCode(StatusCodes.Status503ServiceUnavailable, "Weather service is currently unavailable. Please try again later.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while processing the hourly forecast request");
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again later.");
        }
    }
}

