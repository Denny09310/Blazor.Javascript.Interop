using Microsoft.JSInterop;
using System.Globalization;

namespace Blazor.Javascript.Interop;

public class JSConsole(IJSObjectReference window) : JSInteropBase
{
    public ValueTask AssertAsync(bool condition, params object[] data) => window.InvokeVoidAsync(GetPropertyPath(), condition, data);

    public ValueTask AssertAsync(bool condition, string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(), condition, string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask ClearAsync() => window.InvokeVoidAsync(GetPropertyPath());

    public ValueTask CountAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath()) : window.InvokeVoidAsync(GetPropertyPath(), label);

    public ValueTask CountResetAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath()) : window.InvokeVoidAsync(GetPropertyPath(), label);

    public ValueTask DebugAsync(string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(), string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask DirAsync(object obj) => window.InvokeVoidAsync(GetPropertyPath(), obj);

    public ValueTask DirxmlAsync(object obj) => window.InvokeVoidAsync(GetPropertyPath(), obj);

    public ValueTask ErrorAsync(string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(), string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask GroupAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath()) : window.InvokeVoidAsync(GetPropertyPath(), label);

    public ValueTask GroupCollapsedAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath()) : window.InvokeVoidAsync(GetPropertyPath(), label);

    public ValueTask GroupEndAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath()) : window.InvokeVoidAsync(GetPropertyPath(), label);

    public ValueTask InfoAsync(string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(), string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask LogAsync(string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(), string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask TableAsync(object data) => window.InvokeVoidAsync(GetPropertyPath(), data);

    public ValueTask TimeAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath()) : window.InvokeVoidAsync(GetPropertyPath(), label);

    public ValueTask TimeEndAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath()) : window.InvokeVoidAsync(GetPropertyPath(), label);

    public ValueTask TimeLogAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath()) : window.InvokeVoidAsync(GetPropertyPath(), label);

    public ValueTask TraceAsync(string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(), string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask WarnAsync(string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(), string.Format(CultureInfo.InvariantCulture, message, substitution));
}