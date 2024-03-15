using Blazor.Javascript.Interop.Tests.Fixtures;
using Blazor.Javascript.Interop.Tests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace Blazor.Javascript.Interop.Tests;

public class ConsoleTest : IClassFixture<WebDriverFixture>
{
    private readonly EdgeDriver _driver;
    private readonly BlazorHelper _blazorHelper;

    public ConsoleTest(WebDriverFixture webDriverFixture)
    {
        _driver = webDriverFixture.Driver;
        _blazorHelper = new BlazorHelper(_driver, TimeSpan.FromSeconds(10));

        _driver.Navigate().GoToUrl("http://localhost:5292/features/console");

        _blazorHelper.WaitForBlazorInitialization();
        _blazorHelper.ClearBlazorConsole();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConsoleAssert(bool condition)
    {
        var buttonId = $"window-console-assert-{condition.ToString().ToLower()}-button";
        _driver.FindElement(By.Id(buttonId)).Click();

        var log = GetBrowserConsoleLogs();
        if (condition)
        {
            Assert.Empty(log);
        }
        else
        {
            Assert.Single(log);
            Assert.True(log.First().Level == LogLevel.Severe);
        }
    }

    [Theory]
    [InlineData("debug")]
    [InlineData("info")]
    [InlineData("warning")]
    [InlineData("severe")]
    public void Output(string level)
    {
        var buttonId = $"window-console-{level}-button";
        _driver.FindElement(By.Id(buttonId)).Click();

        var expectedMessage = $"This is a {level} message";
        var log = GetBrowserConsoleLogs();
        Assert.Single(log);
        Assert.Contains(expectedMessage, log.First().Message);
    }

    [Fact]
    public void Clear()
    {
        _driver.FindElement(By.Id("window-console-clear-button")).Click();
        var log = GetBrowserConsoleLogs();
        Assert.Empty(log);
    }

    [Fact]
    public void Time()
    {
        var timeButton = _driver.FindElement(By.Id("window-console-time-button"));
        _driver.ExecuteScript("arguments[0].scrollIntoView(true);", timeButton);

        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(driver => IsVisibleInViewport(driver, timeButton));

        timeButton.Click();

        _driver.FindElement(By.Id("window-console-time-log-button")).Click();
        _driver.FindElement(By.Id("window-console-time-end-button")).Click();

        var log = GetBrowserConsoleLogs();
        Assert.Equal(2, log.Count);
        Assert.Contains("default: ", log.First().Message);
        Assert.Matches(@"default: \d*\.?\d* ms", log.Last().Message);
    }

    private ReadOnlyCollection<LogEntry> GetBrowserConsoleLogs()
    {
        return _driver.Manage().Logs.GetLog(LogType.Browser);
    }

    public static bool IsVisibleInViewport(IWebDriver driver, IWebElement element)
    {
        return (bool)((IJavaScriptExecutor)driver).ExecuteScript("""
            var elem = arguments[0],
                box = elem.getBoundingClientRect(),
                cx = box.left + box.width / 2,
                cy = box.top + box.height / 2,
                e = document.elementFromPoint(cx, cy);

            for (; e; e = e.parentElement) {
                if (e === elem) return true;
            }

            return false;
            """, element);
    }
}