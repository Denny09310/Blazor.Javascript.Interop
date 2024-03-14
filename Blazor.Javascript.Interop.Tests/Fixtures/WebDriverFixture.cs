using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Blazor.Javascript.Interop.Tests.Fixtures;

public class WebDriverFixture : IDisposable
{
    public EdgeDriver Driver { get; }

    public WebDriverFixture()
    {
        var options = new EdgeOptions();
        options.SetLoggingPreference(LogType.Browser, LogLevel.All);
        Driver = new EdgeDriver(options);
    }

    public void Dispose()
    {
        Driver.Dispose();
        GC.SuppressFinalize(this);
    }
}
