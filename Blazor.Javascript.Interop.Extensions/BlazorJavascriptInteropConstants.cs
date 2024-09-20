namespace Blazor.Javascript.Interop.Extensions;

internal static class BlazorJavascriptInteropConstants
{
    internal const string AddEventListener = $"{LibraryName}.addEventListener";
    internal const string GetProperty = $"{LibraryName}.getProperty";
    internal const string GetReference = $"{LibraryName}.getReference";
    internal const string RemoEventListener = $"{LibraryName}.removeEventListener";
    internal const string SetProperty = $"{LibraryName}.setProperty";

    private const string LibraryName = "BlazorJavascriptInterop";
}