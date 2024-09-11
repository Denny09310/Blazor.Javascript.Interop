namespace Microsoft.JSInterop;

public static class JSObjectReferenceExtensions
{
    public static ValueTask AddEventListenerAsync<T>(this IJSObjectReference reference, string type, Action<T> callback)
    {
        return reference.InvokeVoidAsync("addEventListener", type, DotNetCallbackReference.Create(callback));
    }

    public static ValueTask AddEventListenerAsync<T>(this IJSObjectReference reference, string type, Action<T> callback, object serializationSpec)
    {
        return reference.InvokeVoidAsync("addEventListener", type, DotNetCallbackReference.Create(callback, serializationSpec));
    }

    public static ValueTask AddEventListenerAsync<T>(this IJSObjectReference reference, string type, Func<T, ValueTask> callback)
    {
        return reference.InvokeVoidAsync("addEventListener", type, DotNetCallbackReference.Create(callback));
    }

    public static ValueTask AddEventListenerAsync<T>(this IJSObjectReference reference, string type, Func<T, ValueTask> callback, object serializationSpec)
    {
        return reference.InvokeVoidAsync("addEventListener", type, DotNetCallbackReference.Create(callback, serializationSpec));
    }

    public static ValueTask RemoveEventListenerAsync(this IJSObjectReference reference, string type, Delegate callback)
    {
        return reference.InvokeVoidAsync("removeEventListener", type, DotNetCallbackReference.Create(callback));
    }
}