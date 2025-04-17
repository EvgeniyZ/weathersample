using System.Text.Json.Serialization;

namespace WeatherApp.Models;

/// <summary>
/// Represents the units used for current weather measurements.
/// </summary>
public class CurrentWeatherUnits
{
    /// <summary>
    /// The format of the time values (e.g., "iso8601").
    /// </summary>
    [JsonPropertyName("time")]
    public string Time { get; set; } = string.Empty;

    /// <summary>
    /// The unit of the interval between measurements (e.g., "seconds").
    /// </summary>
    [JsonPropertyName("interval")]
    public string Interval { get; set; } = string.Empty;

    /// <summary>
    /// The unit of the temperature at 2 meters above ground (e.g., "°C").
    /// </summary>
    [JsonPropertyName("temperature_2m")]
    public string Temperature2m { get; set; } = string.Empty;

    /// <summary>
    /// The unit of the wind speed at 10 meters above ground (e.g., "km/h").
    /// </summary>
    [JsonPropertyName("wind_speed_10m")]
    public string WindSpeed10m { get; set; } = string.Empty;
}

/// <summary>
/// Represents the current weather conditions.
/// </summary>
public class CurrentWeather
{
    /// <summary>
    /// The time of the current weather data in ISO 8601 format.
    /// </summary>
    [JsonPropertyName("time")]
    public string Time { get; set; } = string.Empty;

    /// <summary>
    /// The interval between measurements in seconds.
    /// </summary>
    [JsonPropertyName("interval")]
    public int Interval { get; set; }

    /// <summary>
    /// The temperature at 2 meters above ground.
    /// </summary>
    [JsonPropertyName("temperature_2m")]
    public double Temperature2m { get; set; }

    /// <summary>
    /// The wind speed at 10 meters above ground.
    /// </summary>
    [JsonPropertyName("wind_speed_10m")]
    public double WindSpeed10m { get; set; }
}

