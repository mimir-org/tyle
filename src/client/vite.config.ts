/// <reference types="vitest" />
import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
// eslint-disable-next-line import/no-unresolved
import macrosPlugin from "vite-plugin-babel-macros";
import svgrPlugin from "vite-plugin-svgr";
import path from "path";

// https://vitejs.dev/config/
export default defineConfig({
  // This changes the out put dir from dist to build
  // comment this out if that isn't relevant for your project
  build: {
    outDir: "build",
  },
  plugins: [
    react(),
    macrosPlugin(),
    svgrPlugin({
      svgrOptions: {
        icon: true,
        // ...svgr options (https://react-svgr.com/docs/options/)
      },
    }),
  ],
  resolve: {
    alias: {
      fs: require.resolve("rollup-plugin-node-builtins"),
      common: path.resolve("src/common"),
      external: path.resolve("src/external"),
      features: path.resolve("src/features"),
      locales: path.resolve("src/locales"),
    },
  },
  server: {
    open: true,
    port: 3001,
  },
  test: {
    globals: true,
    environment: "jsdom",
    coverage: {
      provider: "istanbul",
      reporter: ["text", "html"],
      exclude: ["node_modules/", "src/setupTests.ts"],
    },
  },
});
