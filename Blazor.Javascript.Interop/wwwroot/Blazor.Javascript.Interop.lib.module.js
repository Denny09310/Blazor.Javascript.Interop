export const beforeStart = beforeStartCallback;
export const beforeWebStart = beforeStartCallback;

function beforeStartCallback() {
    var script = document.createElement('script');
    script.setAttribute('src', './_content/Blazor.Javascript.Interop/index.js');
    document.body.appendChild(script);
}
