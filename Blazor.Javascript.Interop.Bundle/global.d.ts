export { };

declare global {
    /**
     * Represents a utility object for interacting with .NET runtime from JavaScript.
     * @const
     */
    const DotNet: {
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

    /**
     * @interface Credential
     * @description Represents a generic credential.
     * @property {string} id - The identifier of the credential.
     * @property {"password" | "federated" | "public-key" | "identity" | "otp"} type - The type of the credential.
     */
    class Credential {
        id: string;
        type: "password" | "federated" | "public-key" | "identity" | "otp";
    }

    /**
     * @interface PasswordCredential
     * @description Represents a password-based credential.
     * @augments Credential
     * @property {string} iconURL - The URL to the icon associated with the credential.
     * @property {string} name - The name associated with the credential.
     * @property {string} password - The password value of the credential.
     */
    class PasswordCredential extends Credential {
        iconURL: string;
        name: string;
        password: string;
    }

    /**
     * @interface FederatedCredential
     * @description Represents a federated credential.
     * @augments Credential
     * @property {string} protocol - The protocol used for federated authentication.
     * @property {string} provider - The provider of the federated credential.
     */
    class FederatedCredential extends Credential {
        protocol: string;
        provider: string;
    }
}