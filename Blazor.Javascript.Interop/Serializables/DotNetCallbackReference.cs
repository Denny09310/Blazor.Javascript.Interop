using System.Text.Json.Serialization;

namespace Microsoft.JSInterop;

public class DotNetCallbackReference
{
    private DotNetCallbackReference()
    { }

    public object? Callback { get; set; }

    [JsonPropertyName("__isCallBackReference")]
    public string IsCallBackReference { get; set; } = "";

    public static DotNetCallbackReference Create<T>(Func<T, ValueTask> callback) => new()
    {
        Callback = DotNetObjectReference.Create(new JSInteropAsyncCallbackWrapper<T>(callback))
    };

    public static DotNetCallbackReference Create(Func<ValueTask> callback) => new()
    {
        Callback = DotNetObjectReference.Create(new JSInteropAsyncCallbackWrapper(callback))
    };
    
    public static DotNetCallbackReference Create<T>(Action<T> callback) => new()
    {
        Callback = DotNetObjectReference.Create(new JSInteropCallbackWrapper<T>(callback))
    };
    
    public static DotNetCallbackReference Create(Action callback) => new()
    {
        Callback = DotNetObjectReference.Create(new JSInteropCallbackWrapper(callback))
    };

    private class JSInteropAsyncCallbackWrapper(Func<ValueTask> func)
    {
        [JSInvokable]
        public ValueTask Invoke() => func.Invoke();
    }

    private class JSInteropAsyncCallbackWrapper<T>(Func<T, ValueTask> func)
    {
        [JSInvokable]
        public ValueTask Invoke(T args) => func.Invoke(args);
    }

    private class JSInteropCallbackWrapper(Action func)
    {
        [JSInvokable]
        public void Invoke() => func.Invoke();
    }

    private class JSInteropCallbackWrapper<T>(Action<T> func)
    {
        [JSInvokable]
        public void Invoke(T args) => func.Invoke(args);
    }
}