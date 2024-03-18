using Blazor.Javascript.Interop.Models;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop.Extensions;

public static class JSRuntimeExtensions
{
    public static ValueTask<T> GetPropertyAsync<T>(this IJSObjectReference reference, string identifier)
    {
        return reference.InvokeAsync<T>("getProperty", identifier);
    }

    public static ValueTask<IJSObjectReference> GetReferenceAsync(this IJSRuntime jsRuntime, string identifier)
    {
        return jsRuntime.InvokeAsync<IJSObjectReference>("eval", $"window['{identifier}']");
    }

    public static async ValueTask<JSWindow> GetWindowAsync(this IJSRuntime jsRuntime)
    {
        var window = await jsRuntime.GetReferenceAsync("self");
        return new JSWindow(window);
    }

    public static ValueTask SetPropertyAsync<T>(this IJSObjectReference reference, string identifier, T value)
    {
        return reference.InvokeVoidAsync("setProperty", identifier, value);
    }

    public static ValueTask AddEventListenerAsync<T>(this IJSObjectReference reference, string type, Action<EventListenerCallback<T>> callback)
    {
        return reference.InvokeVoidAsync("addEventListener", type, DotNetCallbackReference.Create(callback));
    }

    public static ValueTask AddEventListenerAsync<T>(this IJSObjectReference reference, string type, Func<EventListenerCallback<T>, ValueTask> callback)
    {
        return reference.InvokeVoidAsync("addEventListener", type, DotNetCallbackReference.Create(callback));
    }
}