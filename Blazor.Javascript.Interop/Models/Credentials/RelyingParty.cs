using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Models;

public class RelyingParty
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    public required string Name { get; set; }
}
