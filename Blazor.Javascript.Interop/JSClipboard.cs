using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSClipboard(IJSObjectReference navigator) : JSInteropBase(navigator, "clipboard")
{
    public ValueTask<string> ReadTextAsync() => InvokeAsync<string>("readText");

    public ValueTask WriteTextAsync(string data) => InvokeVoidAsync("writeText", data);
}