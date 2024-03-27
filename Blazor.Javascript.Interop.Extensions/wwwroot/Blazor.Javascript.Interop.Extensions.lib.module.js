export function beforeWebStart() {
    var script = document.createElement('script');
    script.setAttribute('src', './_content/Blazor.Javascript.Interop.Extensions/index.js');
    document.head.appendChild(script);
}