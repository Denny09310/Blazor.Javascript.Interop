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

// lib/index.ts
var BlazorJavascriptInterop = {};
var callbackRegistry = /* @__PURE__ */ new Map();
Object.defineProperty(BlazorJavascriptInterop, "getReference", {
  value: function(element) {
    return element;
  }
});
Object.defineProperty(BlazorJavascriptInterop, "addEventListener", {
  value: (element, type, jsCallback) => {
    element.addEventListener(type, jsCallback);
  }
});
Object.defineProperty(BlazorJavascriptInterop, "removeEventListener", {
  value: (element, type, callbackId) => {
    const jsCallback = callbackRegistry.get(callbackId);
    if (jsCallback) {
      element.removeEventListener(type, jsCallback);
      callbackRegistry.delete(callbackId);
    }
  }
});
DotNet.attachReviver((_, value) => {
  if (value && typeof value === "object" && value.hasOwnProperty("__isCallBackReference")) {
    const { callback, serializationSpec } = value;
    const jsCallback = (...args) => callback.invokeMethodAsync("Invoke", [...args].map((arg) => serialize(arg, [], serializationSpec)));
    const callbackId = value.__callbackId;
    callbackRegistry.set(callbackId, jsCallback);
    return jsCallback;
  }
  return value;
});
