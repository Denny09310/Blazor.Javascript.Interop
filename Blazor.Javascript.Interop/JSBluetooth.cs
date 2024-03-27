using Blazor.Javascript.Interop.Models;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSBluetooth(IJSObjectReference navigator) : JSInteropBase(navigator, "bluetooth")
{
    public ValueTask<bool> GetAvailabiltyAsync() => InvokeAsync<bool>("getAvailability");

    public ValueTask<IEnumerable<BluetoothDevice>> GetDevicesAsync() => InvokeAsync<IEnumerable<BluetoothDevice>>("getDevices");
}