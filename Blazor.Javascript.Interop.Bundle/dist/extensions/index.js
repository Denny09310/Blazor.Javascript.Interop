"use strict";function i(e,r=[],t="*"){if(t===!1||typeof e=="undefined"||e===null||typeof e=="number"||typeof e=="string"||typeof e=="boolean")return e;let f=Array.isArray(e)?[]:{};for(let o in e){if(typeof e[o]=="function"||e[o]===null)continue;let y=t!=="*"?Array.isArray(e)?t:t[o]:"*";if(!y)continue;let n=e[o];if(typeof n=="object"){if(r.includes(n))continue;r.push(n),Array.isArray(n)||typeof n=="string"||typeof n=="boolean"||typeof n=="number"||typeof n=="function"?f[o]=n:f[o]=i(n,r,y)}else f[o]=n===1/0?"Infinity":n}return f}Object.defineProperty(Event.prototype,"toJSON",{value:function(){return i(this)}});DotNet.attachReviver((e,r)=>{if(r&&typeof r=="object"&&r.hasOwnProperty("__isCallBackReference")){let{callback:t}=r;return(...f)=>t.invokeMethodAsync("Invoke",f)}return r});
