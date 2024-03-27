import { serializeObject } from "../utils";

/**
 * Defines a method to serialize a Event object to JSON format.
 * @returns {any} The JSON representation of the PermissionStatus object.
 */
Object.defineProperty(Event.prototype, "toJSON", {
    value: function (): any {
        return serializeObject(this)
    },
});


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
        const { callback } = value as { callback: DotNetObjectReference };
        return (...args: unknown[]) => callback.invokeMethodAsync('Invoke', ...[args]);
    }
    return value;
});