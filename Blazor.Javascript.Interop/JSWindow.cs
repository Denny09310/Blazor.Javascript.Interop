using Blazor.Javascript.Interop.Extensions;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSWindow(IJSObjectReference self)
{
    private readonly Lazy<JSConsole> console = new(() => new JSConsole(self));

    private readonly Lazy<JSStorage> localStorage = new(() => new JSStorage(StorageType.Local, self));
    private readonly Lazy<JSStorage> sessionStorage = new(() => new JSStorage(StorageType.Session, self));

    private readonly Lazy<JSNavigator> navigator = new(() => new JSNavigator(self));

    public JSConsole Console => console.Value;

    public JSStorage LocalStorage => localStorage.Value;
    public JSStorage SessionStorage => sessionStorage.Value;

    public JSNavigator Navigator => navigator.Value;

    public ValueTask<bool> ClosedAsync() => self.GetPropertyAsync<bool>("closed");
    public ValueTask<bool> CredentialLessAsync() => self.GetPropertyAsync<bool>("credentialless");
    public ValueTask<bool> CrossOriginIsolatedAsync() => self.GetPropertyAsync<bool>("crossOriginIsolated");
}