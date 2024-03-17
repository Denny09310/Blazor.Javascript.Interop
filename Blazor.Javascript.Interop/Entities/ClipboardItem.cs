using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Entities;

public class ClipboardItem
{
    [JsonInclude]
    protected IJSObjectReference Reference { get; set; } = null!;

    public PresentationStyle PresentationStyle { get; set; }

    public IReadOnlyCollection<string> Types { get; set; } = [];

    public ValueTask<IEnumerable<byte>> GetTypeAsync(string type) => Reference.InvokeAsync<IEnumerable<byte>>("getType", type);
}

[JsonConverter(typeof(JsonStringEnumConverter<PresentationStyle>))]
public enum PresentationStyle
{
    Unspecified,
    Inline,
    Attachment
}