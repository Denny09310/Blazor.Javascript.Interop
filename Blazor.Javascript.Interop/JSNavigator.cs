using Blazor.Javascript.Interop.Extensions;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSNavigator(IJSObjectReference window) : JSInteropBase(window, "navigator")
{
    private readonly IJSObjectReference window = window;

    private IJSObjectReference? _navigator;
    
    private Lazy<JSBluetooth>? bluetooth;
    private Lazy<JSClipboard>? clipboard;
    private Lazy<JSGeolocation>? geolocation;

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

    public ValueTask<bool> CookieEnabledAsync() => _navigator?.GetPropertyAsync<bool>("cookieEnabled") ?? throw new NotSupportedException("The navigator has not been initialized yet");

    public ValueTask<string> LanguageAsync() => _navigator?.GetPropertyAsync<string>("language") ?? throw new NotSupportedException("The navigator has not been initialized yet");

    public ValueTask<bool> OnlineAsync() => _navigator?.GetPropertyAsync<bool>("onLine") ?? throw new NotSupportedException("The navigator has not been initialized yet");

    public ValueTask<string> PlatformAsync() => _navigator?.GetPropertyAsync<string>("appName") ?? throw new NotSupportedException("The navigator has not been initialized yet");

    public ValueTask<string> UserAgentAsync() => _navigator?.GetPropertyAsync<string>("appName") ?? throw new NotSupportedException("The navigator has not been initialized yet");

    public ValueTask<bool> WebDriverAsync() => _navigator?.GetPropertyAsync<bool>("webDriver") ?? throw new NotSupportedException("The navigator has not been initialized yet");
}