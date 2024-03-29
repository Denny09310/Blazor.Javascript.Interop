﻿using Blazor.Javascript.Interop.Contracts;
using Microsoft.JSInterop;

namespace Blazor.Javascript.Interop;

public class JSStorage(StorageType type, IJSObjectReference window) : JSInteropBase(window, FormatCamelCase(type.ToString() + "Storage")), IStorage
{
    public ValueTask ClearAsync() => InvokeVoidAsync("clear");

    public ValueTask<T> GetItemAsync<T>(string keyName) => InvokeAsync<T>("getItem", keyName);

    public ValueTask<T> KeyAsync<T>(int index) => InvokeAsync<T>("key", index);

    public ValueTask<int> LengthAsync() => GetPropertyAsync<int>("length");

    public ValueTask RemoveItemAsync(string keyName) => InvokeVoidAsync("removeItem", keyName);

    public ValueTask SetItemAsync<T>(string keyName, T keyValue) => InvokeVoidAsync("setItem", keyName, keyValue);
}

public enum StorageType
{
    Local,
    Session
}