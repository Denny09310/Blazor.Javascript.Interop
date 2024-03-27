using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop.Extensions;

public static class JSRuntimeExtensions
{
    public static async ValueTask<JSWindow> GetWindowAsync(this IJSRuntime jsRuntime)
    {
        var window = await jsRuntime.GetReferenceAsync("self");
        return new JSWindow(window);
    }
}