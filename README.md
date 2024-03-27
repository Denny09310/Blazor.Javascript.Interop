# Blazor.Javascript.Interop

## Overview
Blazor.Javascript.Interop is a NuGet package designed as an extension library specifically for Blazor projects. It facilitates seamless interoperability between C# code in Blazor and JavaScript, empowering developers to incorporate JavaScript functionalities into their Blazor applications effortlessly.

## Features
- **Interoperability**: Enables smooth communication between C# code in Blazor and JavaScript.
- **Simplified Integration**: Facilitates easy incorporation of JavaScript functionalities into Blazor applications.
- **Boosted Productivity**: Streamlines development workflows by eliminating complexities in bridging the gap between C# and JavaScript.

## Installation
To install Blazor.Javascript.Interop, simply add the package to your Blazor project via NuGet Package Manager or the .NET CLI:

```bash
dotnet add package Blazor.Javascript.Interop
```

## Usage
After installing the package, you can start using it in your Blazor project by following these steps:

1. **Import the Namespace**: Import the ```Blazor.Javascript.Interop``` namespace in your Blazor components where you intend to utilize JavaScript interop functionalities.

    ```razor
    @using Blazor.Javascript.Interop;
    @using Blazor.Javascript.Interop.Extensions;
    ```

2. **Wrap your Router**: Wrap the router component with the **&lt;WindowCascadingValue&gt;** component

    ```razor
    // Example of invoking a JavaScript function from C# code
    <WindowCascadingValue>
        <Router AppAssembly="typeof(Program).Assembly">
            <Found Context="routeData">
                <RouteView RouteData="routeData" DefaultLayout="typeof(Layout.MainLayout)" />
                <FocusOnNavigate RouteData="routeData" Selector="h1" />
            </Found>
        </Router>
    </WindowCascadingValue>
    ```

3. **Get the JSWindow object**: Utilize the provided methods to invoke JavaScript functions from your C# code.

    ```csharp
    [CascadingParameter]
    public required JSWindow Window { get; set; }
    ```

4. **Use the javascript methods**: Check what are the methods ported to C# from javascript

    ```csharp
    private async Task CookieEnabledAsync() => cookieEnabled = await Window.Navigator.CookieEnabledAsync();
    ```

## Compatibility
Blazor.Javascript.Interop is compatible with Blazor projects targeting both server-side and client-side hosting models.

## Contributing
Contributions to Blazor.Javascript.Interop are welcome! If you have any ideas for improvements, new features, or encounter any issues, feel free to open an issue or submit a pull request on GitHub.

## License
Blazor.Javascript.Interop is licensed under the [MIT License](LICENSE).

## Acknowledgements
This project is inspired by the growing need for seamless integration between C# and JavaScript in Blazor applications. Special thanks to the Blazor community for their continuous support and feedback.

---

**Note**: For more information, detailed examples, and updates, please refer to the [GitHub repository](https://github.com/yourusername/Blazor.Javascript.Interop). We appreciate your interest and hope Blazor.Javascript.Interop proves to be a valuable addition to your Blazor development toolkit!
