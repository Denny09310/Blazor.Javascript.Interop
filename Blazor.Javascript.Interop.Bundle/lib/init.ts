import { serializeObject } from "./utils";

/**
 * Defines a method to convert a Blob object to a Base64 string.
 * @returns {Promise<string>} A promise that resolves with the Base64 string representation of the Blob.
 */
Object.defineProperty(Blob.prototype, "toBase64", {
    value: function (): Promise<string> {
        return new Promise((resolve, _) => {
            const reader = new FileReader();
            reader.onloadend = () => {
                if (typeof reader.result === 'string') {
                    resolve(reader.result.replace("data:text/plain;base64,", ""))
                }
            };
            reader.readAsDataURL(this);
        });
    },
});

/**
 * Defines a method to serialize a ClipboardItem object to JSON format.
 * @returns {any} The JSON representation of the ClipboardItem object.
 */
Object.defineProperty(ClipboardItem.prototype, "toJSON", {
    value: function (): any {
        const serialized = serializeObject(this);
        return { reference: DotNet.createJSObjectReference(this), ...serialized }
    },
});

/**
 * Defines a method to serialize a GeolocationPosition object to JSON format.
 * @returns {any} The JSON representation of the GeolocationPosition object.
 */
Object.defineProperty(GeolocationPosition.prototype, "toJSON", {
    value: function (): any {
        return serializeObject(this);
    },
});

/**
 * Defines a method to retrieve the value of a property from an object.
 * @param {string} key - The key of the property to retrieve.
 * @returns {any} The value of the specified property.
 */
Object.defineProperty(Object.prototype, "getProperty", {
    value: function (key: string): any {
        return this[key];
    },
});

/**
 * Defines a method to set the value of a property on an object.
 * @param {string} key - The key of the property to set.
 * @param {T} value - The value to set for the property.
 * @template T
 */
Object.defineProperty(Object.prototype, "setProperty", {
    value: function <T>(key: string, value: T): void {
        this[key] = value;
    },
});