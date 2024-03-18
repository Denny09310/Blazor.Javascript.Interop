using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSPermissions(IJSObjectReference navigator) : JSInteropBase(navigator, "permissions")
{
}
