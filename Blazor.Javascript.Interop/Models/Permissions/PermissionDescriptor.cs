namespace Blazor.Javascript.Interop.Models;

public class PermissionDescriptor
{
    public required string Name { get; set; }

    public bool UserVisibleOnly { get; set; }

    public bool Sysex { get; set; }
}
