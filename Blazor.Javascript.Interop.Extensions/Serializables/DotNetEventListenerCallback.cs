namespace Microsoft.JSInterop;

public class DotNetEventListenerCallback<T>
{
    public bool IsTrusted { get; set; }
    public T Target { get; set; } = default!;
    public string? Type { get; set; }
    public long EventPhase { get; set; }
    public bool Bubbles { get; set; }
    public bool Cancelable { get; set; }
    public bool DefaultPrevented { get; set; }
    public bool Composed { get; set; }
    public double TimeStamp { get; set; }
    public bool ReturnValue { get; set; }
    public bool CancelBubble { get; set; }
}
