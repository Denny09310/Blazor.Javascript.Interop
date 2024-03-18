namespace Blazor.Javascript.Interop.Models;

public class BluetoothDevice
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public BluetoothRemoteGATTServer Gatt { get; set; } = null!;
}