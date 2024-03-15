using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSWindow(IJSRuntime jsRuntime, IJSObjectReference self)
{
    private readonly Lazy<JSConsole> console = new(() => new JSConsole(self));

    private readonly Lazy<JSStorage> localStorage = new(() => new JSStorage(StorageType.LocalStorage, jsRuntime, self));
    private readonly Lazy<JSStorage> sessionStorage = new(() => new JSStorage(StorageType.SessionStorage, jsRuntime, self));

    private readonly Lazy<JSNavigator> navigator = new(() => new JSNavigator(jsRuntime, self));

    public JSConsole Console => console.Value;

    public JSStorage LocalStorage => localStorage.Value;
    public JSStorage SessionStorage => sessionStorage.Value;

    public JSNavigator Navigator => navigator.Value;
}