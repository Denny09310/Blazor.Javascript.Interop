/**
 * Defines a method to get the value of a property by key on an object.
 * @param key The key of the property.
 * @returns The value of the property.
 */
Object.defineProperty(Object.prototype, "getProperty", {
    value: function getProperty(key: string) {
        return this[key];
    },
    writable: true,
    configurable: true
});

/**
 * Defines a method to set the value of a property by key on an object.
 * @param key The key of the property.
 * @param value The value to set for the property.
 */
Object.defineProperty(Object.prototype, "setProperty", {
    value: function setProperty<T>(key: string, value: T) {
        this[key] = value;
    },
    writable: true,
    configurable: true
});