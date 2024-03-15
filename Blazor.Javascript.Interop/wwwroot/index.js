Object.defineProperty(Object.prototype, "getProperty", {
    value: function getProperty(key) {
        return this[key];
    },
    writable: true,
    configurable: true
});

Object.defineProperty(Object.prototype, "setProperty", {
    value: function setProperty(key, value) {
        this[key] = value;
    },
    writable: true,
    configurable: true
});

DotNet.attachReviver((_, value) => {
    if (value && typeof value === 'object' && value.hasOwnProperty("__isCallBackReference")) {
        return (...args) => value.callback.invokeMethodAsync('Invoke', ...args);
    }
    return value;
});