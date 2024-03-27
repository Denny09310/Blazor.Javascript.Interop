import { defineConfig } from 'tsup'

export default defineConfig((options) => ({
    entry: ["lib/main/index.ts", "lib/extensions/index.ts"],
    format: "cjs",
    minify: !options.watch,
}))