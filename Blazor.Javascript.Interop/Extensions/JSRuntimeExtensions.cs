using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public static class JSRuntimeExtensions
{
    public static async ValueTask<JSWindow> GetWindowAsync(this IJSRuntime jsRuntime)
    {
        var window = await jsRuntime.GetReferenceAsync("self");
        return new JSWindow(window);
    }

    public static ValueTask<IJSObjectReference> GetReferenceAsync(this IJSRuntime jsRuntime, string identifier)
    {
        return jsRuntime.InvokeAsync<IJSObjectReference>("eval", $"window['{identifier}']");
    }

    public static ValueTask<T> GetPropertyAsync<T>(this IJSObjectReference reference, string identifier)
    {
        return reference.InvokeAsync<T>("getProperty", identifier);
    }
}
