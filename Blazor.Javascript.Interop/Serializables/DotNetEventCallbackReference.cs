using Blazor.Javascript.Interop.Contracts;
using Blazor.Javascript.Interop.Models;
using System.Reflection;
using System.Text.Json.Nodes;

namespace Microsoft.JSInterop;

public class DotNetEventCallbackReference : BaseCallbackReference
{
    private DotNetEventCallbackReference()
    { }

    public static DotNetEventCallbackReference Create(IJSObjectReference sender, Delegate callback) => new()
    {
        Callback = CreateCallback(sender, callback)
    };

    protected static object? CreateCallback(IJSObjectReference sender, Delegate func)
    {
        return DotNetObjectReference.Create(new JSInteropEventCallbackWrapper(sender, func));
    }

    private class JSInteropEventCallbackWrapper(IJSObjectReference sender, Delegate func) : JSInteropCallbackWrapper(func)
    {
        protected override object? ParseArgument(JsonNode node, ParameterInfo parameter)
        {
            var obj = base.ParseArgument(node, parameter);

            var parameterType = parameter.ParameterType;

            // Check if parameterType is compatible with EventListenerCallback<>
            if (sender is not null && parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(EventListenerCallback<>))
            {
                // Get the Target property of EventListenerCallback<>
                var targetProperty = parameterType.GetProperty("Target");

                // Ensure the Target property exists and its type is assignable from IReferenceable
                if (targetProperty == null || !typeof(IReferenceable).IsAssignableFrom(targetProperty.PropertyType))
                {
                    throw new InvalidOperationException("The target property doesn't exists or it's type is not a IReferenceable type");
                }

                // Retrieve the value of Target property from the obj
                var referenceObj = targetProperty.GetValue(obj) ?? throw new InvalidOperationException("The target property doesn't exists or IJSRuntime hasn't captured it");

                // Get the Reference property of IReferenceable
                var referenceProperty = typeof(IReferenceable).GetProperty("Reference");

                // Ensure the Reference property exists and set the Reference property to sender
                referenceProperty?.SetValue(referenceObj, sender);

                // Set the target back on the object
                targetProperty.SetValue(obj, referenceObj);
            }

            return obj;
        }
    }
}