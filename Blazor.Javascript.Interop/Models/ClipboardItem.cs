using Blazor.Javascript.Interop.Contracts;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Models;

public class ClipboardItem : IReferenceable
{
    public PresentationStyle PresentationStyle { get; set; }

    public IJSObjectReference Reference { get; init; } = null!;

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