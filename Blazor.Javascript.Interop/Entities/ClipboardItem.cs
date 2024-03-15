namespace Blazor.Javascript.Interop.Entities;

public class ClipboardItem
{
    public PresentationStyle PresentationStyle { get; set; }

    public IReadOnlyCollection<string> Types { get; set; } = [];
}