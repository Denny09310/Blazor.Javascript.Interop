namespace Microsoft.JSInterop;

public class DotNetCallbackReference : BaseCallbackReference
{
    private DotNetCallbackReference()
    { }

    public static DotNetCallbackReference Create(Delegate callback) => new()
    {
        Callback = CreateCallback(callback)
    };
}