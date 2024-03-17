/**
 * Represents a utility object for interacting with .NET runtime from JavaScript.
 * @const
 */
declare const DotNet: {
    /**
     * Invokes a method asynchronously from a .NET assembly.
     * @param {string} assemblyName - The name of the assembly containing the method.
     * @param {string} methodIdentifier - The identifier of the method to invoke.
     * @param {...any} args - Arguments to pass to the method.
     * @returns {Promise<T>} A promise that resolves with the result of the method invocation.
     * @template T
     */
    invokeMethodAsync<T>(assemblyName: string, methodIdentifier: string, ...args: any[]): Promise<T>;

    /**
     * Attaches a reviver function to be used when parsing JSON.
     * @param {(key: string, value: any) => any} reviver - The reviver function to attach.
     */
    attachReviver(reviver: (key: string, value: any) => any): void;

    /**
     * Creates a JavaScript object reference from the provided data.
     * @param {Record<string, object>} data - The data to create the object reference from.
     * @returns {Record<string, object>} The JavaScript object reference.
     */
    createJSObjectReference(data: Record<string, object>): Record<string, object>;
};
