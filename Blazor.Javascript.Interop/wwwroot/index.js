Object.defineProperty(Object.prototype, "getProperty", {
    value: function getProperty(key) {
        return this[key];
    },
    writable: true,
    configurable: true
});

Object.defineProperty(Object.prototype, "setProperty", {
    value: function setProperty(key, value) {
        this[key] = value;
    },
    writable: true,
    configurable: true
});

DotNet.attachReviver((_, value) => {
    if (value && typeof value === 'object' && value.hasOwnProperty("__isCallBackReference")) {
        return (...args) => value.callback.invokeMethodAsync('Invoke', [...serializeObject(args)]);
    }
    return value;
});

function serializeObject(data, alreadySerialized = [], serializationSpec = "*") {
    if (serializationSpec === false || typeof data === "undefined" || data === null || typeof data === "number" || typeof data === "string" || typeof data === "boolean") {
        return data;
    }

    const res = Array.isArray(data) ? [] : {};

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
                res[i] = currentMember.map(arrayItem => typeof arrayItem === 'object' ? serializeObject(arrayItem, alreadySerialized, currentMemberSpec) : arrayItem);
            } else {
                res[i] = currentMember.length === 0 ? [] : serializeObject(currentMember, alreadySerialized, currentMemberSpec);
            }
        } else {
            res[i] = currentMember === Infinity ? "Infinity" : currentMember;
        }
    }

    return res;
}