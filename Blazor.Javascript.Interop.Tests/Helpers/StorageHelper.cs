using OpenQA.Selenium;

namespace Blazor.Javascript.Interop.Tests.Helpers;

internal class StorageHelper(IJavaScriptExecutor javascript, StorageType type)
{
    private readonly string storage = GetStorageName(type);

    public void RemoveItem(string keyName) => javascript.ExecuteScript($"{storage}.removeItem('{keyName}');");

    public bool HasItem(string keyName) => javascript.ExecuteScript($"return {storage}.getItem('{keyName}');") is not null;

    public string GetItem(string keyName) => (string)javascript.ExecuteScript($"return {storage}.getItem('{keyName}');");

    public string GetKey(int keyName) => (string)javascript.ExecuteScript($"return {storage}.key('{keyName}')");
    public long Length() => (long)javascript.ExecuteScript($"return {storage}.length");

    public void SetItem(string keyName, object keyValue) => javascript.ExecuteScript($"{storage}.setItem('{keyName}', {keyValue})");

    public void Clear() => javascript.ExecuteScript($"{storage}.clear()");

    private static string GetStorageName(StorageType type)
    {
        var storageName = type.ToString();
        return char.ToLower(storageName[0]) + storageName[1..];
    }
}


public enum StorageType
{
    LocalStorage,
    SessionStorage,
}