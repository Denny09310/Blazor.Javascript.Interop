using Blazor.Javascript.Interop.Contracts;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop.Models;

public class NavigatorUAData : IReferenceable
{
    public ICollection<NavigatorUADataBrand> Brands { get; set; } = null!;
    public bool Mobile { get; set; }
    public string Platform { get; set; } = null!;

    public IJSObjectReference Reference { get; init; } = null!;

    public ValueTask<IDictionary<string, object>> GetHighEntopyValues(IEnumerable<string> hints) => throw new NotImplementedException();
}