using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop.Extensions;

public static class JSRuntimeExtensions
{
    public static ValueTask<IJSObjectReference> GetReferenceAsync(this IJSRuntime jsRuntime, string identifier)
    {
        return jsRuntime.InvokeAsync<IJSObjectReference>("eval", $"window['{identifier}']");
    }
}