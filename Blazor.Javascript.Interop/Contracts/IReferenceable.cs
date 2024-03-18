using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop.Contracts;

public interface IReferenceable : IAsyncDisposable
{
    public IJSObjectReference Reference { get; init; }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (Reference != null)
        {
            await Reference.DisposeAsync();
        }

        GC.SuppressFinalize(this);
    }
}
