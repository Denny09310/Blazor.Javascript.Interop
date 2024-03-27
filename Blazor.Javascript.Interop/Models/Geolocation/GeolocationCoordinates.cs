using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Models;

public class GeolocationCoordinates
{
    [JsonPropertyName("accuracy")]
    public double Accuracy { get; set; }

    [JsonPropertyName("altitude")]
    public double Altitude { get; set; }

    [JsonPropertyName("altitudeAccuracy")]
    public double AltitudeAccuracy { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("speed")]
    public double Speed { get; set; }
}