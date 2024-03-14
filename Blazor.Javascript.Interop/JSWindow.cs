using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSWindow(IJSRuntime jsRuntime, IJSObjectReference self)
{
    private readonly Lazy<JSConsole> console = new(() => new JSConsole(self));
    private readonly Lazy<JSLocalStorage> localStorage = new(() => new JSLocalStorage(jsRuntime, self));
    private readonly Lazy<JSSessionStorage> sessionStorage = new(() => new JSSessionStorage(jsRuntime, self));

    public JSConsole Console => console.Value;
    public JSLocalStorage LocalStorage => localStorage.Value;
    public JSSessionStorage SessionStorage => sessionStorage.Value;
}
