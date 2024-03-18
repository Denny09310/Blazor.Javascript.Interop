﻿using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Microsoft.JSInterop;

public abstract class BaseCallbackReference
{
    protected BaseCallbackReference()
    { }

    public object? Callback { get; set; }

    [JsonPropertyName("__isCallBackReference")]
    public string IsCallBackReference { get; set; } = "";

    protected static object? CreateCallback(Delegate func)
    {
        return DotNetObjectReference.Create(new JSInteropCallbackWrapper(func));
    }

    protected class JSInteropCallbackWrapper(Delegate func)
    {
        private static readonly JsonSerializerOptions _options = new(JsonSerializerDefaults.Web);

        [JSInvokable]
        public void Invoke(params JsonNode[] nodes)
        {
            var parameters = func.Method.GetParameters();

            if (parameters.Length == 0)
            {
                func.DynamicInvoke();
                return;
            }

            var arguments = new object?[parameters.Length];

            for (int i = 0; i < arguments.Length; i++)
            {
                var (parameter, node) = (parameters[i], nodes[i]);

                arguments[i] = ParseArgument(node, parameter);
            }

            func.DynamicInvoke(arguments);
        }

        protected virtual object? ParseArgument(JsonNode node, ParameterInfo parameter)
        {
            MethodInfo? method, generic;
            var parameterType = parameter.ParameterType;

            if (parameterType.IsPrimitive)
            {
                method = typeof(JsonValue).GetMethod(nameof(JsonValue.GetValue));
                generic = method?.MakeGenericMethod(parameterType);
                return generic?.Invoke(node, null);
            }

            if (parameterType.IsArray)
            {
                method = typeof(JsonArray).GetMethod(nameof(JsonArray.GetValues));
                generic = method?.MakeGenericMethod(parameterType);
                return generic?.Invoke(node, null);
            }

            return node.Deserialize(parameterType, _options);
        }
    }
}