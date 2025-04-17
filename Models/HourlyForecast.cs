using System.Text.Json.Serialization;

namespace WeatherApp.Models;

/// <summary>
/// Represents the units used for hourly weather forecast measurements.
/// </summary>
public class HourlyUnits
{
    /// <summary>
    /// The format of the time values (e.g., "iso8601").
    /// </summary>
    [JsonPropertyName("time")]
    public string Time { get; set; } = string.Empty;

    /// <summary>
    /// The unit of the temperature at 2 meters above ground (e.g., "°C").
    /// </summary>
    [JsonPropertyName("temperature_2m")]
    public string Temperature2m { get; set; } = string.Empty;

    /// <summary>
    /// The unit of the relative humidity at 2 meters above ground (e.g., "%").
    /// </summary>
    [JsonPropertyName("relative_humidity_2m")]
    public string RelativeHumidity2m { get; set; } = string.Empty;

    /// <summary>
    /// The unit of the wind speed at 10 meters above ground (e.g., "km/h").
    /// </summary>
    [JsonPropertyName("wind_speed_10m")]
    public string WindSpeed10m { get; set; } = string.Empty;
}

/// <summary>
/// Represents the hourly weather forecast data.
/// </summary>
public class HourlyForecast
{
    /// <summary>
    /// The timestamps for each forecast hour in ISO 8601 format.
    /// </summary>
    [JsonPropertyName("time")]
    public List<string> Time { get; set; } = new();

    /// <summary>
    /// The temperature at 2 meters above ground for each forecast hour.
    /// </summary>
    [JsonPropertyName("temperature_2m")]
    public List<double> Temperature2m { get; set; } = new();

    /// <summary>
    /// The relative humidity at 2 meters above ground for each forecast hour.
    /// </summary>
    [JsonPropertyName("relative_humidity_2m")]
    public List<double> RelativeHumidity2m { get; set; } = new();

    /// <summary>
    /// The wind speed at 10 meters above ground for each forecast hour.
    /// </summary>
    [JsonPropertyName("wind_speed_10m")]
    public List<double> WindSpeed10m { get; set; } = new();
}

