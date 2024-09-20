"use strict";

// lib/utils.ts
function serialize(data, alreadySerialized = [], serializationSpec = "*") {
  if (serializationSpec === false || typeof data === "undefined" || data === null || typeof data === "number" || typeof data === "string" || typeof data === "boolean") {
    return data;
  }
  const res = Array.isArray(data) ? [] : {};
  for (const key in data) {
    if (typeof data[key] === "function" || data[key] === null) {
      continue;
    }
    const currentMemberSpec = serializationSpec !== "*" ? Array.isArray(data) ? serializationSpec : serializationSpec[key] : "*";
    if (!currentMemberSpec) {
      continue;
    }
    const currentMember = data[key];
    if (typeof currentMember === "object") {
      if (alreadySerialized.includes(currentMember)) {
        continue;
      }
      alreadySerialized.push(currentMember);
      if (Array.isArray(currentMember) || typeof currentMember === "string" || typeof currentMember === "boolean" || typeof currentMember === "number" || typeof currentMember === "function") {
        res[key] = currentMember;
      } else {
        res[key] = serialize(currentMember, alreadySerialized, currentMemberSpec);
      }
    } else {
      res[key] = currentMember === Infinity ? "Infinity" : currentMember;
    }
  }
  return res;
}
var traverse = (element, propertyPath) => {
  const properties = propertyPath.split(".");
  let current = element;
  for (let i = 0; i < properties.length - 1; i++) {
    if (current == null || current[properties[i]] === void 0) {
      throw new Error(`Property '${properties[i]}' is undefined or null on the object at path: '${properties.slice(0, i + 1).join(".")}'`);
    }
    current = current[properties[i]];
  }
  return { current, lastProperty: properties[properties.length - 1] };
};

// lib/index.ts
var BlazorJavascriptInterop = {};
Object.defineProperty(BlazorJavascriptInterop, "getReference", {
  value: (element) => element
});
Object.defineProperty(BlazorJavascriptInterop, "addEventListener", {
  value: (element, type, callback) => {
    element["__handlers__"] ??= {};
    element["__handlers__"][type] ??= [];
    const callbackId = `${type}_${Date.now()}_${Math.random()}`;
    element["__handlers__"][type].push({ callbackId, callback });
    element.addEventListener(type, callback);
    return callbackId;
  }
});
Object.defineProperty(BlazorJavascriptInterop, "removeEventListener", {
  value: (element, type, callbackId) => {
    if (element["__handlers__"] && element["__handlers__"][type]) {
      const handlerIndex = element["__handlers__"][type].findIndex(
        (handler) => handler.callbackId === callbackId
      );
      if (handlerIndex !== -1) {
        const handler = element["__handlers__"][type][handlerIndex];
        element.removeEventListener(type, handler.callback);
        element["__handlers__"][type].splice(handlerIndex, 1);
      }
    }
  }
});
Object.defineProperty(BlazorJavascriptInterop, "setProperty", {
  value: (element, property, value) => {
    const { current, lastProperty } = traverse(element, property);
    current[lastProperty] = value;
  }
});
Object.defineProperty(BlazorJavascriptInterop, "getProperty", {
  value: (element, property) => {
    const { current, lastProperty } = traverse(element, property);
    return current[lastProperty];
  }
});
DotNet.attachReviver((_, value) => {
  if (value && typeof value === "object" && "__isCallBackReference" in value) {
    const { callback, serializationSpec } = value;
    return (...args) => {
      const serializedArgs = args.map((arg) => serialize(arg, [], serializationSpec));
      return callback.invokeMethodAsync("Invoke", serializedArgs);
    };
  }
  return value;
});
