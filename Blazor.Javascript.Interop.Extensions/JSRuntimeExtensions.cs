using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop.Extensions;

public static class JSRuntimeExtensions
{
    public static ValueTask<IJSObjectReference> GetReferenceAsync(this IJSRuntime jsRuntime, string identifier)
    {
        return jsRuntime.InvokeAsync<IJSObjectReference>("eval", $"window['{identifier}']");
    }

    public static ValueTask AddEventListener(this IJSRuntime jsRuntime, ElementReference element, string type, Action callback)
    {
        return jsRuntime.InvokeVoidAsync("addEventCallback", element, type, DotNetEventCallbackReference.Create(default, callback));
    }

    public static ValueTask AddEventListener<T>(this IJSRuntime jsRuntime, ElementReference element, string type, Action<T> callback)
    {
        return jsRuntime.InvokeVoidAsync("addEventCallback", element, type, DotNetEventCallbackReference.Create(default, callback));
    }

    public static ValueTask AddEventListener(this IJSRuntime jsRuntime, ElementReference element, string type, Func<ValueTask> callback)
    {
        return jsRuntime.InvokeVoidAsync("addEventCallback", element, type, DotNetEventCallbackReference.Create(default, callback));
    }

    public static ValueTask AddEventListener<T>(this IJSRuntime jsRuntime, ElementReference element, string type, Func<ValueTask, T> callback)
    {
        return jsRuntime.InvokeVoidAsync("addEventCallback", element, type, DotNetEventCallbackReference.Create(default, callback));
    }
}