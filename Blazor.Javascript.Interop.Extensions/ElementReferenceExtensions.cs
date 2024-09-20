using Blazor.Javascript.Interop.Extensions;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace Microsoft.JSInterop;

public static class ElementReferenceExtensions
{
    public static async ValueTask<string> AddEventListenerAsync(this ElementReference element, string type, Action callback)
    {
        var runtime = element.GetJSRuntime();
        return await runtime.InvokeAsync<string>(BlazorJavascriptInteropConstants.AddEventListener, element, type, DotNetCallbackReference.Create(callback));
    }

    public static async ValueTask<string> AddEventListenerAsync<T>(this ElementReference element, string type, Action<T> callback)
    {
        var serializationSpec = GetSerializationSpec<T>();

        var runtime = element.GetJSRuntime();
        return await runtime.InvokeAsync<string>(BlazorJavascriptInteropConstants.AddEventListener, element, type, DotNetCallbackReference.Create(callback, serializationSpec));
    }

    public static async ValueTask<string> AddEventListenerAsync<T>(this ElementReference element, string type, Action<T> callback, object serializationSpec)
    {
        var runtime = element.GetJSRuntime();
        return await runtime.InvokeAsync<string>(BlazorJavascriptInteropConstants.AddEventListener, element, type, DotNetCallbackReference.Create(callback, serializationSpec));
    }

    public static async ValueTask<string> AddEventListenerAsync(this ElementReference element, string type, Func<ValueTask> callback)
    {
        var runtime = element.GetJSRuntime();
        return await runtime.InvokeAsync<string>(BlazorJavascriptInteropConstants.AddEventListener, element, type, DotNetCallbackReference.Create(callback));
    }

    public static async ValueTask<string> AddEventListenerAsync<T>(this ElementReference element, string type, Func<T, ValueTask> callback)
    {
        var serializationSpec = GetSerializationSpec<T>();

        var runtime = element.GetJSRuntime();
        return await runtime.InvokeAsync<string>(BlazorJavascriptInteropConstants.AddEventListener, element, type, DotNetCallbackReference.Create(callback, serializationSpec));
    }

    public static async ValueTask<string> AddEventListenerAsync<T>(this ElementReference element, string type, Func<T, ValueTask> callback, object serializationSpec)
    {
        var runtime = element.GetJSRuntime();
        return await runtime.InvokeAsync<string>(BlazorJavascriptInteropConstants.AddEventListener, element, type, DotNetCallbackReference.Create(callback, serializationSpec));
    }

    public static async ValueTask<T> GetPropertyAsync<T>(this ElementReference element, string name)
    {
        var runtime = element.GetJSRuntime();
        return await runtime.InvokeAsync<T>(BlazorJavascriptInteropConstants.GetProperty, element, name);
    }

    public static async ValueTask<T> InvokeAsync<T>(this ElementReference element, string identifier, params object?[]? args)
    {
        var reference = await GetJsObjectReference(element);
        return await reference.InvokeAsync<T>(identifier, args);
    }

    public static async ValueTask InvokeVoidAsync(this ElementReference element, string identifier, params object?[]? args)
    {
        var reference = await GetJsObjectReference(element);
        await reference.InvokeVoidAsync(identifier, args);
    }

    public static async ValueTask RemoveEventListenerAsync(this ElementReference element, string type, string callbackId)
    {
        var runtime = element.GetJSRuntime();
        await runtime.InvokeVoidAsync(BlazorJavascriptInteropConstants.RemoEventListener, element, type, callbackId);
    }

    public static async ValueTask SetPropertyAsync(this ElementReference element, string name, object value)
    {
        var runtime = element.GetJSRuntime();
        await runtime.InvokeVoidAsync(BlazorJavascriptInteropConstants.SetProperty, element, name, value);
    }

    internal static IJSRuntime GetJSRuntime(this ElementReference elementReference)
    {
        if (elementReference.Context is WebElementReferenceContext context)
        {
            return GetJsRuntime(context);
        }

        throw new InvalidOperationException("No JavaScript runtime found.");
    }

    private static async Task<IJSObjectReference> GetJsObjectReference(ElementReference element)
    {
        var runtime = element.GetJSRuntime();
        return await runtime.InvokeAsync<IJSObjectReference>(BlazorJavascriptInteropConstants.GetReference, element);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "<JSRuntime>k__BackingField")]
    private static extern ref IJSRuntime GetJsRuntime(WebElementReferenceContext context);

    private static Dictionary<string, object> GetSerializationSpec<T>()
    {
        return GetSerializationSpecRecursive(typeof(T));
    }

    private static Dictionary<string, object> GetSerializationSpecRecursive(Type type)
    {
        // Initialize a dictionary to store the serialization spec
        var result = new Dictionary<string, object>();

        // Iterate over the properties of the current type
        foreach (var property in type.GetProperties())
        {
            // Convert the property name to camelCase
            var propertyName = char.ToLower(property.Name[0]) + property.Name[1..];

            // Check if the property type is a primitive, string, or value type (int, bool, etc.)
            if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string) || property.PropertyType.IsValueType)
            {
                // Add the primitive or value type to the result with "*" as the value
                result[propertyName] = "*";
            }
            else
            {
                // For complex types (non-primitive), recursively get their serialization spec
                result[propertyName] = GetSerializationSpecRecursive(property.PropertyType);
            }
        }

        return result;
    }
}