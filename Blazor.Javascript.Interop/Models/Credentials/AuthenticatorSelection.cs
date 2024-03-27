using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Models;

[JsonConverter(typeof(JsonConverter<Attestation>))]
public enum Attestation
{
    None,
    Direct,
    Enterprise,
    Indirect
}

[JsonConverter(typeof(JsonConverter<UserVerification>))]
public enum UserVerification
{
    Preferred,
    Required,
    Discouraged,
}

public class AuthenticatorSelection
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AuthenticatorAttachment { get; set; }

    public bool RequireResidentKey { get; set; }

    public ResidentKey ResidentKey { get; set; }

    public UserVerification UserVerification { get; set; }
}


[JsonConverter(typeof(JsonConverter<ResidentKey>))]
public enum ResidentKey
{
    Required,
    Preferred,
    Discouraged,
}