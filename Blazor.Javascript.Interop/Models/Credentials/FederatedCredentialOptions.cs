using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Models;

public class FederatedCredentialOptions(string id, string provider) : CredentialOptionsBase(id)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Protocol { get; set; }

    public string Provider { get; set; } = provider;
}
