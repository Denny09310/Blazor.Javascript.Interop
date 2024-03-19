using Blazor.Javascript.Interop.Extensions;
using Blazor.Javascript.Interop.Models;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSNavigator(IJSObjectReference window) : JSInteropBase(window, "navigator")
{
    private readonly IJSObjectReference window = window;

    private IJSObjectReference? _navigator;

    private Lazy<JSBluetooth>? bluetooth;
    private Lazy<JSClipboard>? clipboard;
    private Lazy<JSCredentials>? credentials;
    private Lazy<JSGeolocation>? geolocation;
    private Lazy<JSPermissions>? permissions;

    public JSBluetooth Bluetooth => bluetooth?.Value ?? throw new NotSupportedException("The navigator has not been initialized yet");
    public JSClipboard Clipboard => clipboard?.Value ?? throw new NotSupportedException("The navigator has not been initialized yet");
    public JSCredentials Credentials => credentials?.Value ?? throw new NotSupportedException("The navigator has not been initialized yet");
    public JSGeolocation Geolocation => geolocation?.Value ?? throw new NotSupportedException("The navigator has not been initialized yet");
    public JSPermissions Permissions => permissions?.Value ?? throw new NotSupportedException("The navigator has not been initialized yet");

    public async ValueTask InitializeAsync()
    {
        _navigator = await window.GetPropertyAsync<IJSObjectReference>("navigator");

        bluetooth = new(() => new JSBluetooth(_navigator));
        clipboard = new(() => new JSClipboard(_navigator));
        credentials = new(() => new JSCredentials(_navigator));
        geolocation = new(() => new JSGeolocation(_navigator));
        permissions = new(() => new JSPermissions(_navigator));
    }

    #region Properties

    public ValueTask<bool> CookieEnabledAsync() => GetPropertyAsync<bool>("cookieEnabled");

    public ValueTask<double> DeviceMemoryAsync() => GetPropertyAsync<double>("deviceMemory");

    public ValueTask<int> HardwareConcurrencyAsync() => GetPropertyAsync<int>("hardwareConcurrency");

    public ValueTask<string> LanguageAsync() => GetPropertyAsync<string>("language");

    public ValueTask<IEnumerable<string>> LanguagesAsync() => GetPropertyAsync<IEnumerable<string>>("languages");

    public ValueTask<int> MaxTouchPointsAsync() => GetPropertyAsync<int>("maxTouchPoints");

    public ValueTask<bool> OnlineAsync() => GetPropertyAsync<bool>("onLine");

    public ValueTask<bool> PdfViewerEnabledAsync() => GetPropertyAsync<bool>("pdfViewerEnabled");

    public ValueTask<string> UserAgentAsync() => GetPropertyAsync<string>("userAgent");

    public ValueTask<NavigatorUAData> UserAgentDataAsync() => GetPropertyAsync<NavigatorUAData>("userAgentData");

    public ValueTask<bool> WebDriverAsync() => GetPropertyAsync<bool>("webdriver");

    #endregion Properties

    private new ValueTask<T> GetPropertyAsync<T>(string propertyName) => _navigator?.GetPropertyAsync<T>(propertyName) ?? throw new NotSupportedException("The navigator has not been initialized yet");
}