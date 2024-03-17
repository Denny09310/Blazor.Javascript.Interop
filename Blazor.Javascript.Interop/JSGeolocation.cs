using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSGeolocation(IJSObjectReference navigator) : JSInteropBase(navigator, "geolocation")
{
    public ValueTask ClearWatchAsync(int watchId) => InvokeVoidAsync("clearWatch", watchId);

    public ValueTask GetCurrentPositionAsync(Action<GeolocationPosition> success) => InvokeVoidAsync("getCurrentPosition", DotNetCallbackReference.Create(success));
    public ValueTask GetCurrentPositionAsync(Func<GeolocationPosition, ValueTask> success) => InvokeVoidAsync("getCurrentPosition", DotNetCallbackReference.Create(success));

    public ValueTask<int> WatchPositionAsync(Action<GeolocationPosition> success) => InvokeAsync<int>("watchPosition", DotNetCallbackReference.Create(success));
    public ValueTask<int> WatchPositionAsync(Func<GeolocationPosition, ValueTask> success) => InvokeAsync<int>("watchPosition", DotNetCallbackReference.Create(success));
}