using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSClipboard(IJSObjectReference navigator) : JSInteropBase(navigator, "clipboard")
{

}
