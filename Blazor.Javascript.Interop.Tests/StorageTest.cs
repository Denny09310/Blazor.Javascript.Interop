using Blazor.Javascript.Interop.Tests.Fixtures;
using Blazor.Javascript.Interop.Tests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace Blazor.Javascript.Interop.Tests;

public class StorageTest : IClassFixture<WebDriverFixture>
{
    private readonly EdgeDriver _driver;
    private readonly StorageHelper _localStorage, _sessionStorage;

    public StorageTest(WebDriverFixture webDriverFixture)
    {
        _driver = webDriverFixture.Driver;
        _driver.Navigate().GoToUrl("http://localhost:5292/functionalities/storage");

        _localStorage = new(_driver, StorageType.LocalStorage);
        _sessionStorage = new(_driver, StorageType.SessionStorage);
    }

    [Theory]
    [InlineData(StorageType.LocalStorage), InlineData(StorageType.SessionStorage)]
    public void Clear(StorageType type)
    {
        if (type == StorageType.SessionStorage)
        {
            ClickStorageToggle();
        }

        var storage = GetStorageHelper(type);

        // Set the sample object and check if it exists
        storage.SetItem("sample-item", 1);
        Assert.True(storage.HasItem("sample-item"));

        // Click the clear button
        _driver.FindElement(By.Id("window-local-storage-clear-button")).Click();

        // Check that the storage has been cleared out
        Assert.True(storage.Length() == 0);
    }

    private void ClickStorageToggle()
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        var clearButton = wait.Until(driver => driver.FindElement(By.Id("storage-type-toggle")));
        clearButton.Click();
    }

    private StorageHelper GetStorageHelper(StorageType type) => type switch
    {
        StorageType.LocalStorage => _localStorage,
        StorageType.SessionStorage => _sessionStorage,
        _ => throw new NotImplementedException()
    };
}