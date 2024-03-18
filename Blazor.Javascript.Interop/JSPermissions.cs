using Blazor.Javascript.Interop.Models;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSPermissions(IJSObjectReference navigator) : JSInteropBase(navigator, "permissions")
{
    public ValueTask<PermissionStatus> QueryAsync(PermissionDescriptor descriptor) => InvokeAsync<PermissionStatus>("query", descriptor);
}
