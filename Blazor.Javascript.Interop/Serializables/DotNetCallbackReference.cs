using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Microsoft.JSInterop;

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

                if (parameter.ParameterType.IsPrimitive)
                {
                    var method = typeof(JsonValue).GetMethod(nameof(JsonValue.GetValue));
                    var generic = method?.MakeGenericMethod(parameter.ParameterType);
                    arguments[i] = generic?.Invoke(node, null);
                }

                var obj = Activator.CreateInstance(parameter.ParameterType);

                var json = JsonSerializer.Serialize(node, _options);
                arguments[i] = JsonSerializer.Deserialize(json, parameter.ParameterType, _options);
            }

            func.DynamicInvoke(arguments);
        }
    }
}