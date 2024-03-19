using Blazor.Javascript.Interop.Models;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSCredentials(IJSObjectReference navigator) : JSInteropBase(navigator, "credentials")
{
    public ValueTask<PasswordCredential> CreateAsync(PasswordCredentialOptions? options) => InvokeAsync<PasswordCredential>("create", new { Password = options });

    public ValueTask<FederatedCredential> CreateAsync(FederatedCredentialOptions? options) => InvokeAsync<FederatedCredential>("create", new { Federated = options });

    public ValueTask<FederatedCredential> CreateAsync(PublicKeyCredentialOptions? options) => InvokeAsync<FederatedCredential>("create", new { PublicKey = options });
}