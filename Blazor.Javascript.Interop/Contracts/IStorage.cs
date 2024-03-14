namespace Blazor.Javascript.Interop.Contracts;

public interface IStorage
{
    ValueTask ClearAsync();

    ValueTask<T> GetItemAsync<T>(string keyName);

    ValueTask<string> KeyAsync(int index);

    ValueTask<int> LengthAsync();

    ValueTask RemoveItemAsync(string keyName);

    ValueTask SetItemAsync<T>(string keyName, T keyValue);
}