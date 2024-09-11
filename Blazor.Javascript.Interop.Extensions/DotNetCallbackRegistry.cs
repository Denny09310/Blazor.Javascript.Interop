using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Blazor.Javascript.Interop.Extensions;

public static class DotNetCallbackRegistry
{
    private static readonly ConcurrentDictionary<int, string> _callbackRegistry = new();

    public static void RemoveCallback(Delegate callback)
    {
        _callbackRegistry.TryRemove(callback.GetHashCode(), out _);
    }

    public static bool TryAdd(Delegate callback, string id)
    {
        return _callbackRegistry.TryAdd(callback.GetHashCode(), id);
    }

    public static bool TryGetCallbackId(Delegate callback, [MaybeNullWhen(false)] out string callbackId)
    {
        return _callbackRegistry.TryGetValue(callback.GetHashCode(), out callbackId);
    }
}