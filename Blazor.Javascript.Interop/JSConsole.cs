using Microsoft.JSInterop;
using System.Globalization;

namespace Blazor.Javascript.Interop;

public class JSConsole(IJSObjectReference window) : JSInteropBase(window, "console")
{
    public ValueTask AssertAsync(bool condition, params object[] data) => InvokeVoidAsync("assert", condition, data);

    public ValueTask AssertAsync(bool condition, string message, params object[] substitution) => InvokeVoidAsync("assert", condition, string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask ClearAsync() => InvokeVoidAsync("clear");

    public ValueTask CountAsync(string? label = default) => string.IsNullOrEmpty(label) ? InvokeVoidAsync("count") : InvokeVoidAsync("count", label);

    public ValueTask CountResetAsync(string? label = default) => string.IsNullOrEmpty(label) ? InvokeVoidAsync("countReset") : InvokeVoidAsync("countReset", label);

    public ValueTask DebugAsync(string message, params object[] substitution) => InvokeVoidAsync("debug", string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask DirAsync(object obj) => InvokeVoidAsync("dir", obj);

    public ValueTask DirXmlAsync(object obj) => InvokeVoidAsync("dirxml", obj);

    public ValueTask ErrorAsync(string message, params object[] substitution) => InvokeVoidAsync("error", string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask GroupAsync(string? label = default) => string.IsNullOrEmpty(label) ? InvokeVoidAsync("group") : InvokeVoidAsync("group", label);

    public ValueTask GroupCollapsedAsync(string? label = default) => string.IsNullOrEmpty(label) ? InvokeVoidAsync("groupCollapsed") : InvokeVoidAsync("groupCollapsed", label);

    public ValueTask GroupEndAsync(string? label = default) => string.IsNullOrEmpty(label) ? InvokeVoidAsync("groupEnd") : InvokeVoidAsync("groupEnd", label);

    public ValueTask InfoAsync(string message, params object[] substitution) => InvokeVoidAsync("info", string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask LogAsync(string message, params object[] substitution) => InvokeVoidAsync("log", string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask TableAsync(object data) => InvokeVoidAsync("table", data);

    public ValueTask TimeAsync(string? label = default) => string.IsNullOrEmpty(label) ? InvokeVoidAsync("time") : InvokeVoidAsync("time", label);

    public ValueTask TimeEndAsync(string? label = default) => string.IsNullOrEmpty(label) ? InvokeVoidAsync("timeEnd") : InvokeVoidAsync("timeEnd", label);

    public ValueTask TimeLogAsync(string? label = default) => string.IsNullOrEmpty(label) ? InvokeVoidAsync("timeLog") : InvokeVoidAsync("timeLog", label);

    public ValueTask TraceAsync(string message, params object[] substitution) => InvokeVoidAsync("trace", string.Format(CultureInfo.InvariantCulture, message, substitution));

    public ValueTask WarnAsync(string message, params object[] substitution) => InvokeVoidAsync("warn", string.Format(CultureInfo.InvariantCulture, message, substitution));
}