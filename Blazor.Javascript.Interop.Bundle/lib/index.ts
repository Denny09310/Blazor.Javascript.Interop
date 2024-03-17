import { DotNetObjectReference } from "./types";
import { serializeObject } from "./utils";

import "./configuration"

/**
 * Attaches a reviver function to the DotNet object.
 * @param _ Unused parameter (key).
 * @param value The value being revived.
 * @returns The revived value or the original value if not revivable.
 */
DotNet.attachReviver((_, value) => {
    if (value && typeof value === 'object' && value.hasOwnProperty("__isCallBackReference")) {
        const { callback } = value as { callback: DotNetObjectReference };
        return (...args: unknown[]) => callback.invokeMethodAsync('Invoke', [...serializeObject(args)]);
    }
    return value;
});
