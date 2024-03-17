"use strict";
var __defProp = Object.defineProperty;
var __getOwnPropSymbols = Object.getOwnPropertySymbols;
var __hasOwnProp = Object.prototype.hasOwnProperty;
var __propIsEnum = Object.prototype.propertyIsEnumerable;
var __defNormalProp = (obj, key, value) => key in obj ? __defProp(obj, key, { enumerable: true, configurable: true, writable: true, value }) : obj[key] = value;
var __spreadValues = (a, b) => {
  for (var prop in b || (b = {}))
    if (__hasOwnProp.call(b, prop))
      __defNormalProp(a, prop, b[prop]);
  if (__getOwnPropSymbols)
    for (var prop of __getOwnPropSymbols(b)) {
      if (__propIsEnum.call(b, prop))
        __defNormalProp(a, prop, b[prop]);
    }
  return a;
};

// lib/utils.ts
function serializeObject(data, alreadySerialized = [], serializationSpec = "*") {
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
        res[key] = serializeObject(currentMember, alreadySerialized, currentMemberSpec);
      }
    } else {
      res[key] = currentMember === Infinity ? "Infinity" : currentMember;
    }
  }
  return res;
}

// lib/configuration.ts
Object.defineProperty(Object.prototype, "getProperty", {
  value: function getProperty(key) {
    return this[key];
  },
  writable: false,
  configurable: true
});
Object.defineProperty(Object.prototype, "setProperty", {
  value: function setProperty(key, value) {
    this[key] = value;
  },
  writable: false,
  configurable: true
});
Object.defineProperty(ClipboardItem.prototype, "toJSON", {
  value: function toJSON() {
    const serialized = serializeObject(this);
    return __spreadValues({ reference: DotNet.createJSObjectReference(this) }, serialized);
  },
  writable: false,
  configurable: true
});
Object.defineProperty(GeolocationPosition.prototype, "toJSON", {
  value: function toJSON2() {
    return serializeObject(this);
  },
  writable: false,
  configurable: true
});

// lib/index.ts
DotNet.attachReviver((_, value) => {
  if (value && typeof value === "object" && value.hasOwnProperty("__isCallBackReference")) {
    const { callback } = value;
    return (...args) => callback.invokeMethodAsync("Invoke", ...[args]);
  }
  return value;
});
