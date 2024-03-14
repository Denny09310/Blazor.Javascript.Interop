using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSLocalStorage(IJSRuntime jsRuntime, IJSObjectReference window) : JSInteropBase(jsRuntime, window)
{
    public ValueTask ClearAsync() => window.InvokeVoidAsync(GetPropertyPath());

    public ValueTask<T> GetItemAsync<T>(string keyName) => window.InvokeAsync<T>(GetPropertyPath(), keyName);

    public ValueTask<string> KeyAsync(int index) => window.InvokeAsync<string>(GetPropertyPath(), index);

    public ValueTask RemoveItemAsync(string keyName) => window.InvokeVoidAsync(GetPropertyPath(), keyName);

    public ValueTask SetItemAsync<T>(string keyName, T keyValue) => window.InvokeVoidAsync(GetPropertyPath(), keyName, keyValue);

    public ValueTask<int> LengthAsync() => jsRuntime.GetPropertyAsync<int>(GetPropertyPath());
}