using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSGeolocation(IJSObjectReference navigator) : JSInteropBase(navigator, "geolocation")
{
    public ValueTask ClearWatchAsync(int watchId) => InvokeVoidAsync("clearWatch", watchId);

    public ValueTask GetCurrentPositionAsync() => InvokeVoidAsync("getCurrentPosition");

    public ValueTask<int> WatchPosition() => InvokeAsync<int>("watchPosition");
}