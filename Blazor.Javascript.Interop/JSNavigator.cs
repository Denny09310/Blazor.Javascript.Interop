using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSNavigator(IJSRuntime jsRuntime, IJSObjectReference window) : JSInteropBase(window, "navigator")
{
    private Lazy<JSBluetooth>? bluetooth;
    private Lazy<JSClipboard>? clipboard;

    private IJSObjectReference? _navigator;

    public JSBluetooth Bluetooth => bluetooth?.Value ?? throw new NotSupportedException("The navigator has not been initialized yet");
    public JSClipboard Clipboard => clipboard?.Value ?? throw new NotSupportedException("The navigator has not been initialized yet");

    public async ValueTask InitializeAsync()
    {
        _navigator = await jsRuntime.GetReferenceAsync("navigator");

        bluetooth = new(() => new JSBluetooth(_navigator));
        clipboard = new(() => new JSClipboard(_navigator));
    }
}
