using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSNavigator(IJSObjectReference window) : JSInteropBase(window, "navigator")
{
    private readonly IJSObjectReference window = window;

    private Lazy<JSBluetooth>? bluetooth;
    private Lazy<JSClipboard>? clipboard;
    private Lazy<JSGeolocation>? geolocation;

    private IJSObjectReference? _navigator;

    public JSBluetooth Bluetooth => bluetooth?.Value ?? throw new NotSupportedException("The navigator has not been initialized yet");
    public JSClipboard Clipboard => clipboard?.Value ?? throw new NotSupportedException("The navigator has not been initialized yet");
    public JSGeolocation Geolocation => geolocation?.Value ?? throw new NotSupportedException("The navigator has not been initialized yet");

    public async ValueTask InitializeAsync()
    {
        _navigator = await window.GetPropertyAsync<IJSObjectReference>("navigator");

        bluetooth = new(() => new JSBluetooth(_navigator));
        clipboard = new(() => new JSClipboard(_navigator));
        geolocation = new(() => new JSGeolocation(_navigator));
    }
}
