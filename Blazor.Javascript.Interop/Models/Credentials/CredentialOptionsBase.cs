using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Models;

public class CredentialOptionsBase(string id)
{
    [JsonPropertyName("iconURL"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IconUrl { get; set; }

    public string Id { get; set; } = id;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Origin { get; set; }
}
