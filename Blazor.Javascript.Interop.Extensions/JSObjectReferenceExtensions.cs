using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop.Extensions;

public static class JSObjectReferenceExtensions
{
    public static ValueTask AddEventListenerAsync<T>(this IJSObjectReference reference, string type, Action<DotNetEventListenerCallback<T>> callback)
    {
        return reference.InvokeVoidAsync("addEventListener", type, DotNetEventCallbackReference.Create(reference, callback));
    }

    public static ValueTask AddEventListenerAsync<T>(this IJSObjectReference reference, string type, Func<DotNetEventListenerCallback<T>, ValueTask> callback)
    {
        return reference.InvokeVoidAsync("addEventListener", type, DotNetEventCallbackReference.Create(reference, callback));
    }

    public static ValueTask<T> GetPropertyAsync<T>(this IJSObjectReference reference, string identifier)
    {
        return reference.InvokeAsync<T>("getProperty", identifier);
    }

    public static ValueTask SetPropertyAsync<T>(this IJSObjectReference reference, string identifier, T value)
    {
        return reference.InvokeVoidAsync("setProperty", identifier, value);
    }
}