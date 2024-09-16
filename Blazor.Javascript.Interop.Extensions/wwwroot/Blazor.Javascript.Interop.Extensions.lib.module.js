export const beforeStart = beforeStartCallback;
export const beforeWebStart = beforeStartCallback;

function beforeStartCallback() {
    var script = document.createElement('script');
    script.src = './_content/Blazor.Javascript.Interop.Extensions/index.js';
    document.body.appendChild(script);
}
