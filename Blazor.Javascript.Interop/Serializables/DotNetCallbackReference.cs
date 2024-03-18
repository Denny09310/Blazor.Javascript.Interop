using Microsoft.JSInterop;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Serializables;

public class DotNetCallbackReference
{
    private DotNetCallbackReference()
    { }

    public object? Callback { get; set; }

    [JsonPropertyName("__isCallBackReference")]
    public string IsCallBackReference { get; set; } = "";

    public static DotNetCallbackReference Create(Delegate callback) => new()
    {
        Callback = DotNetObjectReference.Create(new JSInteropCallbackWrapper(callback))
    };

    private class JSInteropCallbackWrapper(Delegate func)
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

                MethodInfo? method, generic;
                var parameterType = parameter.ParameterType;

                if (parameterType.IsPrimitive)
                {
                    method = typeof(JsonValue).GetMethod(nameof(JsonValue.GetValue));
                    generic = method?.MakeGenericMethod(parameterType);
                    arguments[i] = generic?.Invoke(node, null);
                }

                if (parameterType.IsArray)
                {
                    method = typeof(JsonArray).GetMethod(nameof(JsonArray.GetValues));
                    generic = method?.MakeGenericMethod(parameterType);
                    arguments[i] = generic?.Invoke(node, null);
                }

                var obj = Activator.CreateInstance(parameterType);
                arguments[i] = node.Deserialize(parameterType, _options);
            }

            func.DynamicInvoke(arguments);
        }
    }
}