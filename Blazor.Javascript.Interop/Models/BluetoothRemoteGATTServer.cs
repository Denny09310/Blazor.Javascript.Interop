namespace Blazor.Javascript.Interop.Models;

public class BluetoothRemoteGATTServer
{
    public bool Connected { get; set; }
    public BluetoothDevice Device { get; set; } = null!;
}