import { SerializationSpec, serialize } from "./utils";

const BlazorJavascriptInterop = {}
const callbackRegistry = new Map();

/**
 * Defines a method to get a JSObjectReference from the DotNet cache.
 * @returns {any} The JSObjectReference representation of the cached object.
 */
Object.defineProperty(BlazorJavascriptInterop, "getReference", {
    value: function (element: any) {
        return element
    },
});

Object.defineProperty(BlazorJavascriptInterop, "addEventListener", {
    value: (element: any, type: string, jsCallback: any) => {
        element.addEventListener(type, jsCallback);
    },
})

Object.defineProperty(BlazorJavascriptInterop, "removeEventListener", {
    value: (element: any, type: string, callbackId: string) => {
        const jsCallback = callbackRegistry.get(callbackId);
        if (jsCallback) {
            element.removeEventListener(type, jsCallback);
            callbackRegistry.delete(callbackId);
        }
    }
})

interface DotNetObjectReference {
    invokeMethodAsync(identifier: string, ...args: any[]): Promise<any>
}

/**
 * Attaches a reviver function to the DotNet object.
 * @param value The value being revived.
 * @returns The revived value or the original value if not revivable.
 */
DotNet.attachReviver((_, value) => {
    if (value && typeof value === 'object' && value.hasOwnProperty("__isCallBackReference")) {
        const { callback, serializationSpec } = value as { serializationSpec: SerializationSpec, callback: DotNetObjectReference };
        const jsCallback = (...args: any[]) => callback.invokeMethodAsync('Invoke', [...args].map(arg => serialize(arg, [], serializationSpec)));

        const callbackId = value.__callbackId;
        callbackRegistry.set(callbackId, jsCallback);

        return jsCallback;
    }
    return value;
});