using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop;

public class GeolocationPosition
{
    [JsonPropertyName("coords")]
    public GeolocationCoordinates Coords { get; set; } = null!;

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }
}
