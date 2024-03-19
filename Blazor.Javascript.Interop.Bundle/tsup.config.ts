import { defineConfig } from 'tsup'

export default defineConfig((options) => ({
    entry: ["lib/index.ts"],
    format: "cjs",
    outDir: "../Blazor.Javascript.Interop/wwwroot",
    minify: !options.watch,
}))