using System.Text.Json.Serialization;

namespace Blazor.Javascript.Interop.Models;

public class PublicKeyCredentialOptions
{
    public Attestation Attestation { get; set; }

    public IEnumerable<string> AttestationFotmats { get; set; } = [];

    public required byte[] Challenge { get; set; }

    public IEnumerable<ExcludeCredentials> ExcludeCredentials { get; set; } = [];

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Extensions { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<string>? Hints { get; set; }

    public required IEnumerable<PubKeyCredParams> PubKeyCredParams { get; set; }

    public required RelyingParty Rp { get; set; }

    [JsonPropertyName("authenticatorSelection"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AuthenticatorSelection? Selection { get; set; }
    public long? Timeout { get; set; }

    public required User User { get; set; }
}