using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Blazor.Javascript.Interop.Tests.Helpers;

public class BlazorHelper(IWebDriver driver, TimeSpan timeout)
{
    public void WaitForBlazorInitialization()
    {
        var wait = new WebDriverWait(driver, timeout);
        wait.Until(driver => (bool)((IJavaScriptExecutor)driver).ExecuteScript("return window.Blazor !== undefined"));
    }

    public void ClearBlazorConsole()
    {
        ((IJavaScriptExecutor)driver).ExecuteScript("console.clear()");
        driver.Manage().Logs.GetLog(LogType.Browser);
    }
}
