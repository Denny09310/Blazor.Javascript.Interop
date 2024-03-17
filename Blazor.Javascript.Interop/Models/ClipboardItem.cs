using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Models;

public class ClipboardItem
{
    [JsonInclude]
    protected IJSObjectReference Reference { get; set; } = null!;

    public PresentationStyle PresentationStyle { get; set; }

    public IReadOnlyCollection<string> Types { get; set; } = [];

    public async ValueTask<byte[]> GetTypeAsync(string type)
    {
        var blob = await Reference.InvokeAsync<IJSObjectReference>("getType", type);
        return await blob.InvokeAsync<byte[]>("toBase64");
    }
}

[JsonConverter(typeof(JsonStringEnumConverter<PresentationStyle>))]
public enum PresentationStyle
{
    Unspecified,
    Inline,
    Attachment
}