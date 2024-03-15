using Blazor.Javascript.Interop.Entities;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSClipboard(IJSObjectReference navigator) : JSInteropBase(navigator, "clipboard")
{
    public ValueTask<IEnumerable<ClipboardItem>> ReadAsync() => InvokeAsync<IEnumerable<ClipboardItem>>("read");

    public ValueTask<string> ReadTextAsync() => InvokeAsync<string>("readText");

    public ValueTask WriteAsync(IEnumerable<ClipboardItem> data) => InvokeVoidAsync("write", data);

    public ValueTask WriteTextAsync(string data) => InvokeVoidAsync("writeText", data);
}