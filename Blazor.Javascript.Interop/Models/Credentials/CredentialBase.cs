using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Models;

public class CredentialBase
{
    public string Id { get; set; } = null!;

    public CredentialType Type { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter<CredentialType>))]
public enum CredentialType
{
    Federated,
    Password,
}