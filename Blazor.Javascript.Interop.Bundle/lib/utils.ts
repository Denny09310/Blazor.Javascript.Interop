export type SerializationSpec = Record<string, any> | string | boolean;

/**
 * Serializes an object according to the given serialization specifications.
 * @param data The data to be serialized.
 * @param alreadySerialized An array containing already serialized objects to avoid circular references.
 * @param serializationSpec The specification for serialization.
 * @returns The serialized object.
 */
export function serialize<T extends Record<string, any>>(data: T, alreadySerialized: any[] = [], serializationSpec: SerializationSpec = "*"): T {
    if (serializationSpec === false || typeof data === "undefined" || data === null || typeof data === "number" || typeof data === "string" || typeof data === "boolean") {
        return data;
    }

    const res: any = Array.isArray(data) ? [] : {};

    for (const key in data) {
        if (typeof data[key] === 'function' || data[key] === null) {
            continue;
        }

        const currentMemberSpec = serializationSpec !== "*" ? (Array.isArray(data) ? serializationSpec : serializationSpec[key as keyof typeof serializationSpec]) : "*";

        if (!currentMemberSpec) {
            continue;
        }

        const currentMember = data[key];

        if (typeof currentMember === 'object') {
            if (alreadySerialized.includes(currentMember)) {
                continue;
            }
            alreadySerialized.push(currentMember);

            if (Array.isArray(currentMember) || typeof currentMember === 'string' || typeof currentMember === 'boolean' || typeof currentMember === 'number' || typeof currentMember === 'function') {
                res[key] = currentMember;
            } else {
                res[key] = serialize(currentMember, alreadySerialized, currentMemberSpec);
            }
        } else {
            res[key] = currentMember === Infinity ? "Infinity" : currentMember;
        }
    }

    return res as T;
}

export const traverse = (element: any, propertyPath: string): any => {
    const properties = propertyPath.split('.');
    let current = element;

    for (let i = 0; i < properties.length - 1; i++) {
        if (current == null || current[properties[i] as any] === undefined) {
            throw new Error(`Property '${properties[i]}' is undefined or null on the object at path: '${properties.slice(0, i + 1).join('.')}'`);
        }
        current = current[properties[i] as any];
    }

    return { current, lastProperty: properties[properties.length - 1] };
};
