using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Blazor.Javascript.Interop;

public abstract class JSInteropBase(IJSRuntime _, IJSObjectReference __)
{
    protected static string GetPropertyPath([CallerFilePath] string? className = default!, [CallerMemberName] string? methodName = default!)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(className);
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);

        className = Path.GetFileNameWithoutExtension(className)
            .Replace("JS", string.Empty);

        methodName = methodName.Replace("Async", string.Empty);

        return string.Join('.', FormatCamelCase(className), FormatCamelCase(methodName));
    }

    private static string FormatCamelCase(string source) => char.ToLower(source[0]) + source[1..];
}
