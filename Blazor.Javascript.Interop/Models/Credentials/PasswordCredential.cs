using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Models;

public class PasswordCredential : CredentialBase
{
    [JsonPropertyName("iconURL")]
    public string IconUrl { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;
}
