using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public abstract class JSInteropBase(IJSObjectReference parent, string propertyName)
{
    public ValueTask<TValue> GetPropertyAsync<TValue>(string property) => InvokeAsync<TValue>("getProperty", property);

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, params object?[]? args) => parent.InvokeAsync<TValue>(GetPropertyPath(identifier), args);

    public ValueTask InvokeVoidAsync(string identifier, params object?[]? args) => parent.InvokeVoidAsync(GetPropertyPath(identifier), args);

    protected static string FormatCamelCase(string source) => char.ToLower(source[0]) + source[1..];

    private string GetPropertyPath(params string[] methodPath) => $"{propertyName}." + string.Join(".", methodPath);
}