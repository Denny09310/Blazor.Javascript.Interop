namespace Blazor.Javascript.Interop.Models;

public class ExcludeCredentials
{
    public required IEnumerable<byte> Id { get; set; }
    public required IEnumerable<string> Transports { get; set; }
    public required string Type { get; set; }
}
