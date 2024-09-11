using Blazor.Javascript.Interop.Extensions;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.JSInterop;

public static class ElementReferenceExtensions
{
    public static async ValueTask AddEventListenerAsync<T>(this ElementReference element, string type, Action<T> callback)
    {
        var jsRuntime = GetJSRuntime(element) ?? throw new InvalidOperationException("No JavaScript runtime found.");
        var serializationSpec = GetSerializationSpec<T>();

        await jsRuntime.InvokeVoidAsync("BlazorJavascriptInterop.addEventListener", element, type, DotNetCallbackReference.Create(callback, serializationSpec));
    }

    public static async ValueTask AddEventListenerAsync<T>(this ElementReference element, string type, Action<T> callback, object serializationSpec)
    {
        var jsRuntime = GetJSRuntime(element) ?? throw new InvalidOperationException("No JavaScript runtime found.");
        await jsRuntime.InvokeVoidAsync("BlazorJavascriptInterop.addEventListener", element, type, DotNetCallbackReference.Create(callback, serializationSpec));
    }

    public static async ValueTask AddEventListenerAsync<T>(this ElementReference element, string type, Func<T, ValueTask> callback)
    {
        var jsRuntime = GetJSRuntime(element) ?? throw new InvalidOperationException("No JavaScript runtime found.");
        var serializationSpec = GetSerializationSpec<T>();

        await jsRuntime.InvokeVoidAsync("BlazorJavascriptInterop.addEventListener", element, type, DotNetCallbackReference.Create(callback, serializationSpec));
    }

    public static async ValueTask AddEventListenerAsync<T>(this ElementReference element, string type, Func<T, ValueTask> callback, object serializationSpec)
    {
        var jsRuntime = GetJSRuntime(element) ?? throw new InvalidOperationException("No JavaScript runtime found.");
        await jsRuntime.InvokeVoidAsync("BlazorJavascriptInterop.addEventListener", element, type, DotNetCallbackReference.Create(callback, serializationSpec));
    }

    public static async ValueTask<T> InvokeAsync<T>(this ElementReference element, string identifier, params object[]? args)
    {
        var reference = await GetJsObjectReference(element);
        return await reference.InvokeAsync<T>(identifier, args);
    }

    public static async ValueTask InvokeVoidAsync(this ElementReference element, string identifier, params object[]? args)
    {
        var reference = await GetJsObjectReference(element);
        await reference.InvokeVoidAsync(identifier, args);
    }

    public static async ValueTask RemoveEventListenerAsync(this ElementReference element, string type, Delegate callback)
    {
        if (!DotNetCallbackRegistry.TryGetCallbackId(callback, out var callbackId))
        {
            throw new InvalidOperationException("Delegate has no registered callback in the registry.");
        }

        var jsRuntime = GetJSRuntime(element) ?? throw new InvalidOperationException("No JavaScript runtime found.");
        await jsRuntime.InvokeVoidAsync("BlazorJavascriptInterop.removeEventListener", element, type, callbackId);
    }

    internal static IJSRuntime? GetJSRuntime(this ElementReference elementReference)
    {
        if (elementReference.Context is WebElementReferenceContext context)
        {
            var jsRuntime = GetJsRuntime(context);
            return jsRuntime;
        }

        return null;
    }

    private static async Task<IJSObjectReference> GetJsObjectReference(ElementReference element)
    {
        var jsRuntime = GetJSRuntime(element) ?? throw new InvalidOperationException("No JavaScript runtime found.");
        return await jsRuntime.InvokeAsync<IJSObjectReference>("BlazorJavascriptInterop.getReference", element);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "<JSRuntime>k__BackingField")]
    private static extern ref IJSRuntime GetJsRuntime(WebElementReferenceContext context);

    private static Dictionary<string, string> GetSerializationSpec<T>()
    {
        return typeof(T)
            .GetProperties()
            .GroupBy(property => property.Name)
            .ToDictionary(group => char.ToLower(group.Key[0]) + group.Key[1..], _ => "*");
    }
}