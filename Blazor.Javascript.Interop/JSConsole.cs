using Microsoft.JSInterop;
using System.Globalization;

namespace Blazor.Javascript.Interop;

public class JSConsole(IJSObjectReference window) : JSInteropBase
{
    private const string _propertyName = "console";

    public ValueTask AssertAsync(bool condition, params object[] data) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "assert"), condition, data);

    public ValueTask AssertAsync(bool condition, string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "assert"), condition, string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask ClearAsync() => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "clear"));

    public ValueTask CountAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath(_propertyName, "count")) : window.InvokeVoidAsync(GetPropertyPath(_propertyName, "count"), label);

    public ValueTask CountResetAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath(_propertyName, "countReset")) : window.InvokeVoidAsync(GetPropertyPath(_propertyName, "countReset"), label);

    public ValueTask DebugAsync(string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "debug"), string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask DirAsync(object obj) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "dir"), obj);

    public ValueTask DirXmlAsync(object obj) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "dirxml"), obj);

    public ValueTask ErrorAsync(string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "error"), string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask GroupAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath(_propertyName, "group")) : window.InvokeVoidAsync(GetPropertyPath(_propertyName, "group"), label);

    public ValueTask GroupCollapsedAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath(_propertyName, "groupCollapsed")) : window.InvokeVoidAsync(GetPropertyPath(_propertyName, "groupCollapsed"), label);

    public ValueTask GroupEndAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath(_propertyName, "groupEnd")) : window.InvokeVoidAsync(GetPropertyPath(_propertyName, "groupEnd"), label);

    public ValueTask InfoAsync(string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "info"), string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask LogAsync(string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "log"), string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask TableAsync(object data) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "table"), data);

    public ValueTask TimeAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath(_propertyName, "time")) : window.InvokeVoidAsync(GetPropertyPath(_propertyName, "time"), label);

    public ValueTask TimeEndAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath(_propertyName, "timeEnd")) : window.InvokeVoidAsync(GetPropertyPath(_propertyName, "timeEnd"), label);

    public ValueTask TimeLogAsync(string? label = default) => string.IsNullOrEmpty(label) ? window.InvokeVoidAsync(GetPropertyPath(_propertyName, "timeLog")) : window.InvokeVoidAsync(GetPropertyPath(_propertyName, "timeLog"), label);

    public ValueTask TraceAsync(string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "trace"), string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask WarnAsync(string message, params object[] substitution) => window.InvokeVoidAsync(GetPropertyPath(_propertyName, "warn"), string.Format(CultureInfo.InvariantCulture, message, substitution));
}