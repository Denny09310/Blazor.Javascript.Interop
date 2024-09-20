import { SerializationSpec, serialize, traverse } from "./utils";

// Define an interface for BlazorJavascriptInterop
interface BlazorJavascriptInteropType {
    getReference: (element: HTMLElement) => HTMLElement;
    addEventListener: (element: HTMLElement, type: string, callback: EventListener) => void;
    removeEventListener: (element: HTMLElement, type: string, callbackId: string) => void;
    setProperty: (element: HTMLElement, property: string, value: any) => void;
    getProperty: (element: HTMLElement, property: string) => any;
}

// Define the BlazorJavascriptInterop object with type safety
const BlazorJavascriptInterop: BlazorJavascriptInteropType = {} as BlazorJavascriptInteropType;

/**
 * Returns a reference to the provided HTML element.
 * @param element - The HTML element to get the reference for.
 * @returns The same element that was passed.
 */
Object.defineProperty(BlazorJavascriptInterop, "getReference", {
    value: (element: any) => element
});

/**
 * Adds an event listener to the given HTML element.
 * Supports multiple listeners for the same event type.
 * @param element - The HTML element to add the event listener to.
 * @param type - The type of event to listen for (e.g., 'click', 'change').
 * @param callback - The JavaScript callback function to execute when the event is triggered.
 * @returns A unique callback ID.
 */
Object.defineProperty(BlazorJavascriptInterop, "addEventListener", {
    value: (element: any, type: string, callback: EventListener): string => {
        // Ensure __handlers__ object and array for the event type exist
        element["__handlers__"] ??= {};
        element["__handlers__"][type] ??= [];

        // Generate a unique callback ID
        const callbackId = `${type}_${Date.now()}_${Math.random()}`;

        // Store the callback with its ID
        element["__handlers__"][type].push({ callbackId, callback });

        // Add the event listener to the element
        element.addEventListener(type, callback);

        return callbackId; // Return the unique ID
    }
});

/**
 * Removes an event listener from the given HTML element by callback ID.
 * @param element - The HTML element to remove the event listener from.
 * @param type - The type of event that the listener is associated with.
 * @param callbackId - The unique identifier for the callback to remove.
 */
Object.defineProperty(BlazorJavascriptInterop, "removeEventListener", {
    value: (element: any, type: string, callbackId: string) => {
        // Check if handlers exist for the event type
        if (element["__handlers__"] && element["__handlers__"][type]) {
            // Find the index of the callback with the given ID
            const handlerIndex = element["__handlers__"][type].findIndex(
                (handler: { callbackId: string }) => handler.callbackId === callbackId
            );

            if (handlerIndex !== -1) {
                // Retrieve and remove the handler
                const handler = element["__handlers__"][type][handlerIndex];
                element.removeEventListener(type, handler.callback);

                // Remove the handler from the array
                element["__handlers__"][type].splice(handlerIndex, 1);
            }
        }
    }
});

/**
 * Sets a property on the given HTML element, supporting dot-separated property paths.
 * @param element - The HTML element to set the property on.
 * @param propertyPath - The dot-separated property path to be set (e.g., 'style.color').
 * @param value - The value to assign to the property.
 */
Object.defineProperty(BlazorJavascriptInterop, "setProperty", {
    value: (element: HTMLElement, property: string, value: any) => {
        const { current, lastProperty } = traverse(element, property);

        // Set the value on the final property
        current[lastProperty] = value;
    }
});

/**
 * Retrieves the value of a property from the given HTML element.
 * @param element - The HTML element to get the property from.
 * @param property - The name of the property to retrieve (e.g., 'value', 'innerHTML').
 * @returns The value of the property.
 */
Object.defineProperty(BlazorJavascriptInterop, "getProperty", {
    value: (element: HTMLElement, property: string): any => {
        const { current, lastProperty } = traverse(element, property);

        // Get the value of the final property
        return current[lastProperty];
    }
});

// Define an interface for DotNetObjectReference
interface DotNetObjectReference {
    invokeMethodAsync(identifier: string, ...args: any[]): Promise<any>;
}

/**
 * Attaches a reviver function to the DotNet object, allowing for JavaScript callbacks
 * to be registered and invoked by .NET code when needed.
 * @param value - The value being revived, which may contain a callback reference.
 * @returns Either the revived JavaScript callback or the original value if it's not revivable.
 */
DotNet.attachReviver((_: unknown, value: any) => {
    if (value && typeof value === 'object' && '__isCallBackReference' in value) {
        const { callback, serializationSpec } = value as {
            callback: DotNetObjectReference;
            serializationSpec: SerializationSpec;
        };

        // Define the JavaScript callback that will invoke the .NET method asynchronously
        return (...args: any[]) => {
            const serializedArgs = args.map(arg => serialize(arg, [], serializationSpec));
            return callback.invokeMethodAsync('Invoke', serializedArgs);
        };
    }
    return value;
});