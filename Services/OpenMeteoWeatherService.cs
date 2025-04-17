using System.Net.Http.Json;
using System.Text.Json;
using WeatherApp.Models;

namespace WeatherApp.Services;

/// <summary>
/// Service that retrieves weather data from the Open Meteo API.
/// </summary>
public class OpenMeteoWeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OpenMeteoWeatherService> _logger;
    private const string BaseUrl = "https://api.open-meteo.com/v1/forecast";

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenMeteoWeatherService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client used to make API requests.</param>
    /// <param name="logger">Logger for diagnostic information.</param>
    public OpenMeteoWeatherService(HttpClient httpClient, ILogger<OpenMeteoWeatherService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets weather data for a specific location defined by coordinates.
    /// </summary>
    /// <param name="latitude">The latitude of the location.</param>
    /// <param name="longitude">The longitude of the location.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>A task that resolves to a WeatherResponse object containing weather data.</returns>
    /// <exception cref="HttpRequestException">Thrown when the HTTP request fails.</exception>
    /// <exception cref="JsonException">Thrown when JSON deserialization fails.</exception>
    /// <exception cref="TaskCanceledException">Thrown when the operation is canceled.</exception>
    public async Task<WeatherResponse> GetWeatherDataAsync(double latitude, double longitude, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Fetching weather data for coordinates: Lat {Latitude}, Lon {Longitude}", latitude, longitude);

            // Build query parameters for the API request
            var queryParameters = new Dictionary<string, string>
            {
                ["latitude"] = latitude.ToString(),
                ["longitude"] = longitude.ToString(),
                ["current"] = "temperature_2m,wind_speed_10m",
                ["hourly"] = "temperature_2m,relative_humidity_2m,wind_speed_10m"
            };

            // Construct the query string
            var queryString = string.Join("&", queryParameters.Select(kv => $"{kv.Key}={kv.Value}"));
            var requestUrl = $"{BaseUrl}?{queryString}";

            // Make the API request
            var response = await _httpClient.GetAsync(requestUrl, cancellationToken);
            
            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Deserialize the response
            var weatherData = await response.Content.ReadFromJsonAsync<WeatherResponse>(
                options: null, 
                cancellationToken: cancellationToken);

            if (weatherData == null)
            {
                throw new InvalidOperationException("Failed to deserialize weather data from the API response.");
            }

            _logger.LogInformation("Successfully retrieved weather data for coordinates: Lat {Latitude}, Lon {Longitude}", latitude, longitude);
            return weatherData;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed while fetching weather data for coordinates: Lat {Latitude}, Lon {Longitude}", latitude, longitude);
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Failed to deserialize JSON response while fetching weather data for coordinates: Lat {Latitude}, Lon {Longitude}", latitude, longitude);
            throw;
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogWarning(ex, "Request for weather data was canceled for coordinates: Lat {Latitude}, Lon {Longitude}", latitude, longitude);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching weather data for coordinates: Lat {Latitude}, Lon {Longitude}", latitude, longitude);
            throw;
        }
    }
}

