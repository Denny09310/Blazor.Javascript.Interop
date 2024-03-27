namespace Blazor.Javascript.Interop.Models;

public class PasswordCredentialOptions(string id, string password) : CredentialOptionsBase(id)
{
    public string Password { get; set; } = password;
}