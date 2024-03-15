using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSNavigator(IJSRuntime jsRuntime, IJSObjectReference window) : JSInteropBase
{
    private const string _propertyName = "navigator";

    private Lazy<JSBluetooth>? bluetooth;
    private IJSObjectReference? _navigator;

    public JSBluetooth Bluetooth => bluetooth?.Value ?? throw new NotSupportedException("The navigator has not been initialized yet");

    public async ValueTask InitializeAsync()
    {
        _navigator = await jsRuntime.GetReferenceAsync("navigator");
        bluetooth = new(() => new JSBluetooth(jsRuntime, _navigator));
    }
}
