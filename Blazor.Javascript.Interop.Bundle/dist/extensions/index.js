"use strict";function c(e,t=[],o="*"){if(o===!1||typeof e=="undefined"||e===null||typeof e=="number"||typeof e=="string"||typeof e=="boolean")return e;let i=Array.isArray(e)?[]:{};for(let r in e){if(typeof e[r]=="function"||e[r]===null)continue;let f=o!=="*"?Array.isArray(e)?o:o[r]:"*";if(!f)continue;let n=e[r];if(typeof n=="object"){if(t.includes(n))continue;t.push(n),Array.isArray(n)||typeof n=="string"||typeof n=="boolean"||typeof n=="number"||typeof n=="function"?i[r]=n:i[r]=c(n,t,f)}else i[r]=n===1/0?"Infinity":n}return i}Object.defineProperty(Event.prototype,"toJSON",{value:function(){return c(this)}});Object.defineProperty(PointerEvent.prototype,"toJSON",{value:function(){return c(this)}});Object.defineProperty(Window.prototype,"addEventCallback",{value:function(e,t,o){e.addEventListener(t,o)}});DotNet.attachReviver((e,t)=>{if(t&&typeof t=="object"&&t.hasOwnProperty("__isCallBackReference")){let{callback:o}=t;return(...i)=>o.invokeMethodAsync("Invoke",i)}return t});
