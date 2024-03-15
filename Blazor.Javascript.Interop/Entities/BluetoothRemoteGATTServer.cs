namespace Blazor.Javascript.Interop.Entities;

public class BluetoothRemoteGATTServer
{
    public bool Connected { get; set; }
    public BluetoothDevice Device { get; set; } = null!;
}