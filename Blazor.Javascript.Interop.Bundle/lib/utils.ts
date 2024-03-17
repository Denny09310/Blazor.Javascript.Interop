
/**
 * Serializes an object according to the given serialization specifications.
 * @param data The data to be serialized.
 * @param alreadySerialized An array containing already serialized objects to avoid circular references.
 * @param serializationSpec The specification for serialization.
 * @returns The serialized object.
 */
export function serializeObject(data: any, alreadySerialized: any[] = [], serializationSpec: any = "*"): any {
    if (serializationSpec === false || typeof data === "undefined" || data === null || typeof data === "number" || typeof data === "string" || typeof data === "boolean") {
        return data;
    }

    const res: any = Array.isArray(data) ? [] : {};

    for (const i in data) {
        if (typeof data[i] === 'function' || data[i] === null) {
            continue;
        }

        const currentMemberSpec = serializationSpec !== "*" ? (Array.isArray(data) ? serializationSpec : serializationSpec[i]) : "*";

        if (!currentMemberSpec) {
            continue;
        }

        const currentMember = data[i];

        if (typeof currentMember === 'object') {
            if (alreadySerialized.includes(currentMember)) {
                continue;
            }
            alreadySerialized.push(currentMember);

            if (Array.isArray(currentMember) || currentMember.length) {
                res[i] = currentMember.map((arrayItem: any) => typeof arrayItem === 'object' ? serializeObject(arrayItem, alreadySerialized, currentMemberSpec) : arrayItem);
            } else {
                res[i] = currentMember.length === 0 ? [] : serializeObject(currentMember, alreadySerialized, currentMemberSpec);
            }
        } else {
            res[i] = currentMember === Infinity ? "Infinity" : currentMember;
        }
    }

    return res;
}

/**
 * Deserializes an object according to the given serialization specifications.
 * @param data The data to be deserialized.
 * @param serializationSpec The specification for deserialization.
 * @returns The deserialized object.
 */
export function deserializeObject(data: any, serializationSpec: any = "*"): any {
    if (serializationSpec === false || typeof data === "undefined" || data === null || typeof data === "number" || typeof data === "string" || typeof data === "boolean") {
        return data;
    }

    const res: any = Array.isArray(data) ? [] : {};

    for (const key in data) {
        if (typeof data[key] === 'function' || data[key] === null) {
            continue;
        }

        const currentMemberSpec = serializationSpec !== "*" ? (Array.isArray(data) ? serializationSpec : serializationSpec[key]) : "*";

        if (!currentMemberSpec) {
            continue;
        }

        const currentMember = data[key];

        if (typeof currentMember === 'object') {
            if (Array.isArray(currentMember) || currentMember.length) {
                res[key] = currentMember.map((arrayItem: any) => typeof arrayItem === 'object' ? deserializeObject(arrayItem, currentMemberSpec) : arrayItem);
            } else {
                res[key] = currentMember.length === 0 ? [] : deserializeObject(currentMember, currentMemberSpec);
            }
        } else {
            res[key] = currentMember === "Infinity" ? Infinity : currentMember;
        }
    }

    return res;
}
