namespace Blazor.Javascript.Interop.Extensions.Helpers;

internal static class SerializationHelper
{
    internal static Dictionary<string, object> GetSerializationSpec<T>()
    {
        return GetSerializationSpecRecursive(typeof(T));
    }

    internal static Dictionary<string, object> GetSerializationSpecRecursive(Type type)
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
