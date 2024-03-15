namespace Blazor.Javascript.Interop;

public abstract class JSInteropBase
{
    protected static string GetPropertyPath(string propertyName, params string[] methodPath) => $"{propertyName}." + string.Join(".", methodPath);

    protected static string FormatCamelCase(string source) => char.ToLower(source[0]) + source[1..];
}
