"use strict";function s(e,n=[],r="*"){if(r===!1||typeof e>"u"||e===null||typeof e=="number"||typeof e=="string"||typeof e=="boolean")return e;let i=Array.isArray(e)?[]:{};for(let o in e){if(typeof e[o]=="function"||e[o]===null)continue;let c=r!=="*"?Array.isArray(e)?r:r[o]:"*";if(!c)continue;let t=e[o];if(typeof t=="object"){if(n.includes(t))continue;n.push(t),Array.isArray(t)||typeof t=="string"||typeof t=="boolean"||typeof t=="number"||typeof t=="function"?i[o]=t:i[o]=s(t,n,c)}else i[o]=t===1/0?"Infinity":t}return i}var y={},f=new Map;Object.defineProperty(y,"getReference",{value:function(e){return e}});Object.defineProperty(y,"addEventListener",{value:(e,n,r)=>{e.addEventListener(n,r)}});Object.defineProperty(y,"removeEventListener",{value:(e,n,r)=>{let i=f.get(r);i&&(e.removeEventListener(n,i),f.delete(r))}});DotNet.attachReviver((e,n)=>{if(n&&typeof n=="object"&&n.hasOwnProperty("__isCallBackReference")){let{callback:r,serializationSpec:i}=n,o=(...t)=>r.invokeMethodAsync("Invoke",[...t].map(a=>s(a,[],i))),c=n.__callbackId;return f.set(c,o),o}return n});
