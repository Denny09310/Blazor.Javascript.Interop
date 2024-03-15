using Blazor.Javascript.Interop.Contracts;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSStorage(StorageType type, IJSRuntime jsRuntime, IJSObjectReference window) : JSInteropBase, IStorage
{
    private readonly string _propertyName = FormatCamelCase(type.ToString());

    public ValueTask ClearAsync() => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "clear"));

    public ValueTask<T> GetItemAsync<T>(string keyName) => window.InvokeAsync<T>(GetPropertyPath(_propertyName, "getItem"), keyName);

    public ValueTask<string> KeyAsync(int index) => window.InvokeAsync<string>(GetPropertyPath(_propertyName, "key"), index);

    public ValueTask<int> LengthAsync() => jsRuntime.GetPropertyAsync<int>(GetPropertyPath(_propertyName, "length"));

    public ValueTask RemoveItemAsync(string keyName) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "removeItem"), keyName);

    public ValueTask SetItemAsync<T>(string keyName, T keyValue) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "setItem"), keyName, keyValue);
}