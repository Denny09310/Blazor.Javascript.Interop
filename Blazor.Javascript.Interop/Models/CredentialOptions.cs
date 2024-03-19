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

public class FederatedCredentialOptions(string id, string provider) : CredentialOptionsBase(id)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Protocol { get; set; }

    public string Provider { get; set; } = provider;
}

public class PasswordCredentialOptions(string id, string password) : CredentialOptionsBase(id)
{
    public string Password { get; set; } = password;
}