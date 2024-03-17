import { serializeObject } from "./utils";

Object.defineProperty(Object.prototype, "getProperty", {
    value: function getProperty(key: string) {
        return this[key];
    },
});

Object.defineProperty(Object.prototype, "setProperty", {
    value: function setProperty<T>(key: string, value: T) {
        this[key] = value;
    },
});

Object.defineProperty(ClipboardItem.prototype, "toJSON", {
    value: function toJSON(): any {
        const serialized = serializeObject(this);
        return { reference: DotNet.createJSObjectReference(this), ...serialized }
    },
});

Object.defineProperty(GeolocationPosition.prototype, "toJSON", {
    value: function toJSON(): any {
        return serializeObject(this);
    },
});

Object.defineProperty(Blob.prototype, "toBase64", {
    value: function () {
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
})