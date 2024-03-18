using Blazor.Javascript.Interop.Contracts;
using Blazor.Javascript.Interop.Extensions;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Models;

public class PermissionStatus : IReferenceable
{
    public string Name { get; set; } = null!;
    public PermissionState State { get; set; }
    
    public IJSObjectReference Reference { get; init; } = null!;

    public ValueTask OnChangeAsync(Action<EventListenerCallback<PermissionStatus>> action) => Reference.AddEventListenerAsync("change", action);

    public ValueTask OnChangeAsync(Func<EventListenerCallback<PermissionStatus>, ValueTask> func) => Reference.AddEventListenerAsync("change", func);
}

[JsonConverter(typeof(JsonStringEnumConverter<PermissionState>))]
public enum PermissionState
{
    Granted,
    Denied,
    Prompt
}