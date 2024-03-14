using Blazor.Javascript.Interop.Tests.Fixtures;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Blazor.Javascript.Interop.Tests;

public class ConsoleTest : IClassFixture<WebDriverFixture>
{
    private readonly EdgeDriver driver;

    public ConsoleTest(WebDriverFixture webDriverFixture)
    {
        driver = webDriverFixture.Driver;
        driver.Navigate().GoToUrl("http://localhost:5292/functionalities/console");

        driver.ExecuteScript("console.clear()");
        driver.Manage().Logs.GetLog(LogType.Browser);
    }

    [Theory]
    [InlineData(true), InlineData(false)]
    public void AssertWithCondition(bool condition)
    {
        var buttonId = $"window-console-assert-{condition}-button".ToLower();
        driver.FindElement(By.Id(buttonId)).Click();

        var log = driver.Manage().Logs.GetLog(LogType.Browser);

        if (condition)
        {
            Assert.Empty(log);
            return;
        }

        var entry = log.LastOrDefault();

        Assert.NotNull(entry);
        Assert.True(entry.Level == LogLevel.Severe);
    }

    [Theory]
    [InlineData(LogLevel.Debug), InlineData(LogLevel.Info), InlineData(LogLevel.Warning), InlineData(LogLevel.Severe)]
    public void OutputWithLevel(LogLevel level)
    {
        var buttonId = string.Format("window-console-{0}-button", $"{level}".ToLower());
        driver.FindElement(By.Id(buttonId)).Click();

        var log = driver.Manage().Logs.GetLog(LogType.Browser);
        var entry = log.LastOrDefault();

        Assert.NotNull(entry);
        Assert.True(entry.Level == level);
    }

    //[Fact]
    //public void OuputTable()
    //{
    //    driver.FindElement(By.Id("window-console-table-button")).Click();

    //    var log = driver.Manage().Logs.GetLog(LogType.Browser);
    //    var message = log.LastOrDefault();

    //    Assert.NotNull(message);
    //    Assert.Equal(LogLevel.Info, message.Level);

    //    Assert.Contains("apples", message.Message);
    //    Assert.Contains("oranges", message.Message);
    //    Assert.Contains("bananas", message.Message);
    //}

    [Fact]
    public void Clear()
    {
        driver.FindElement(By.Id("window-console-clear-button")).Click();

        var log = driver.Manage().Logs.GetLog(LogType.Browser);
        Assert.Empty(log);
    }

    //[Theory]
    //[InlineData(null), InlineData("timer")]
    //public void CountWithLabel(string? label)
    //{
    //    var buttonId = "window-console-count-" + (string.IsNullOrWhiteSpace(label) ? string.Empty : "with-label-") + "button";

    //    for (int i = 0; i < 10; i++)
    //    {
    //        driver.FindElement(By.Id(buttonId)).Click();

    //        var log = driver.Manage().Logs.GetLog(LogType.Browser);
    //        var entry = log.LastOrDefault();

    //        Assert.NotNull(entry);
    //        Assert.Contains(string.IsNullOrEmpty(label) ? "default" : label, entry.Message);
    //    }
    //}
}