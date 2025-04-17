using WeatherApp.Models;

namespace WeatherApp.Services;

/// <summary>
/// Interface for a service that retrieves weather data from external APIs.
/// </summary>
public interface IWeatherService
{
    /// <summary>
    /// Gets weather data for a specific location defined by coordinates.
    /// </summary>
    /// <param name="latitude">The latitude of the location.</param>
    /// <param name="longitude">The longitude of the location.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>A task that resolves to a WeatherResponse object containing weather data.</returns>
    Task<WeatherResponse> GetWeatherDataAsync(double latitude, double longitude, CancellationToken cancellationToken = default);
}

