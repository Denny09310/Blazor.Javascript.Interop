using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSCredentials(IJSObjectReference navigator) : JSInteropBase(navigator, "credentials")
{
}
