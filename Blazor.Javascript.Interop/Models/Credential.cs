using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Models;

public class CredentialBase
{
    public string Id { get; set; } = null!;

    public CredentialType Type { get; set; }
}

public class FederatedCredential : CredentialBase
{
    public string Protocol { get; set; } = null!;

    public string Provider { get; set; } = null!;
}

public class PasswordCredential : CredentialBase
{
    [JsonPropertyName("iconURL")]
    public string IconUrl { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;
}

[JsonConverter(typeof(JsonStringEnumConverter<CredentialType>))]
public enum CredentialType
{
    Federated,
    Password,
}