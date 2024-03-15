using Blazor.Javascript.Interop.Entities;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSBluetooth(IJSRuntime jsRuntime, IJSObjectReference parent) : JSInteropBase
{
    private const string _propertyName = "bluetooth";

    public ValueTask<bool> GetAvailabiltyAsync() => parent.InvokeAsync<bool>(GetPropertyPath(_propertyName, "getAvailability"));

    public ValueTask<IEnumerable<BluetoothDevice>> GetDevicesAsync() => parent.InvokeAsync<IEnumerable<BluetoothDevice>>(GetPropertyPath(_propertyName, "getDevices"));
}
