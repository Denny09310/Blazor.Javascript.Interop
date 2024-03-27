using Blazor.Javascript.Interop.Extensions;
using Blazor.Javascript.Interop.Models;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSWindow(IJSObjectReference self) : IAsyncDisposable
{
    private readonly Lazy<JSConsole> _console = new(() => new JSConsole(self));
    private readonly Lazy<JSStorage> _localStorage = new(() => new JSStorage(StorageType.Local, self));
    private readonly Lazy<JSNavigator> _navigator = new(() => new JSNavigator(self));
    private readonly Lazy<JSStorage> _sessionStorage = new(() => new JSStorage(StorageType.Session, self));

    public JSConsole Console => _console.Value;
    public JSStorage LocalStorage => _localStorage.Value;
    public JSNavigator Navigator => _navigator.Value;
    public JSStorage SessionStorage => _sessionStorage.Value;

    #region Properties

    public ValueTask<bool> ClosedAsync() => self.GetPropertyAsync<bool>("closed");

    public ValueTask<bool> CredentialLessAsync() => self.GetPropertyAsync<bool>("credentialless");

    public ValueTask<bool> CrossOriginIsolatedAsync() => self.GetPropertyAsync<bool>("crossOriginIsolated");

    #endregion Properties

    public async ValueTask DisposeAsync()
    {
        await Navigator.DisposeAsync();

        await self.DisposeAsync();
        
        GC.SuppressFinalize(this);
    }
}