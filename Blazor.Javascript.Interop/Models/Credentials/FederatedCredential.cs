namespace Blazor.Javascript.Interop.Models;

public class FederatedCredential : CredentialBase
{
    public string Protocol { get; set; } = null!;

    public string Provider { get; set; } = null!;
}
