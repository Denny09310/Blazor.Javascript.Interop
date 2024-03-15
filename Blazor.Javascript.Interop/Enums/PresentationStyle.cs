using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop;

[JsonConverter(typeof(JsonStringEnumConverter<PresentationStyle>))]
public enum PresentationStyle
{
    Unspecified,
    Inline,
    Attachment
}
