using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop.Contracts;

public interface IReferenceable
{
    public IJSObjectReference Reference { get; init; }
}
