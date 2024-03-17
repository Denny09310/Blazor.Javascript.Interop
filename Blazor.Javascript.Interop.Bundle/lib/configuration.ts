import { serialize } from "v8";
import { serializeObject } from "./utils";

/**
 * Defines a method to get the value of a property by key on an object.
 * @param key The key of the property.
 * @returns The value of the property.
 */
Object.defineProperty(Object.prototype, "getProperty", {
    value: function getProperty(key: string) {
        return this[key];
    },
    writable: false,
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
    writable: false,
    configurable: true
});

Object.defineProperty(ClipboardItem.prototype, "toJSON", {
    value: function toJSON(): any {
        const serialized = serializeObject(this);
        return { reference: DotNet.createJSObjectReference(this), ...serialized }
    },
    writable: false,
    configurable: true
});

Object.defineProperty(GeolocationPosition.prototype, "toJSON", {
    value: function toJSON(): any {
        return serializeObject(this);   
    },
    writable: false,
    configurable: true
});