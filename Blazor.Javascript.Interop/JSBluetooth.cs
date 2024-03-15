using Blazor.Javascript.Interop.Entities;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSBluetooth(IJSObjectReference parent) : JSInteropBase(parent, "bluetooth")
{
    public ValueTask<bool> GetAvailabiltyAsync() => InvokeAsync<bool>("getAvailability");

    public ValueTask<IEnumerable<BluetoothDevice>> GetDevicesAsync() => InvokeAsync<IEnumerable<BluetoothDevice>>("getDevices");
}
