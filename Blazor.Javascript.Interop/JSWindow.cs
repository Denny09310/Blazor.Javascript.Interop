using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSWindow(IJSRuntime jsRuntime, IJSObjectReference self)
{
    private readonly Lazy<JSConsole> console = new(() => new JSConsole(jsRuntime, self));

    public JSConsole Console => console.Value;
}
