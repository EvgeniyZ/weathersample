using System.Text.Json.Serialization;

namespace WeatherApp.Models;

/// <summary>
/// Represents the complete response from the Open-Meteo API.
/// Contains geographical information, current weather data, and hourly forecasts.
/// </summary>
public class WeatherResponse
{
    /// <summary>
    /// The latitude of the requested location.
    /// </summary>
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    /// <summary>
    /// The longitude of the requested location.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    /// <summary>
    /// The time in milliseconds taken to generate the weather forecast.
    /// </summary>
    [JsonPropertyName("generationtime_ms")]
    public double GenerationTimeMs { get; set; }

    /// <summary>
    /// The UTC offset in seconds for the requested timezone.
    /// </summary>
    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffsetSeconds { get; set; }

    /// <summary>
    /// The timezone identifier of the requested location.
    /// </summary>
    [JsonPropertyName("timezone")]
    public string Timezone { get; set; } = string.Empty;

    /// <summary>
    /// The abbreviation of the timezone.
    /// </summary>
    [JsonPropertyName("timezone_abbreviation")]
    public string TimezoneAbbreviation { get; set; } = string.Empty;

    /// <summary>
    /// The elevation of the requested location in meters.
    /// </summary>
    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    /// <summary>
    /// Units for the current weather data.
    /// </summary>
    [JsonPropertyName("current_units")]
    public CurrentWeatherUnits CurrentUnits { get; set; } = new();

    /// <summary>
    /// Current weather conditions.
    /// </summary>
    [JsonPropertyName("current")]
    public CurrentWeather Current { get; set; } = new();

    /// <summary>
    /// Units for the hourly forecast data.
    /// </summary>
    [JsonPropertyName("hourly_units")]
    public HourlyUnits HourlyUnits { get; set; } = new();

    /// <summary>
    /// Hourly weather forecast data.
    /// </summary>
    [JsonPropertyName("hourly")]
    public HourlyForecast Hourly { get; set; } = new();
}

