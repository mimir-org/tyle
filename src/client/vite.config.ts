/// <reference types="vitest" />
import react from "@vitejs/plugin-react";
import { defineConfig } from "vite";
// eslint-disable-next-line import/no-unresolved
import path from "path";
// eslint-disable-next-line import/no-unresolved
import macrosPlugin from "vite-plugin-babel-macros";
import svgrPlugin from "vite-plugin-svgr";

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
      api: path.resolve("src/api"),
      components: path.resolve("src/components"),
      config: path.resolve("src/config.ts"),
      helpers: path.resolve("src/helpers"),
      hooks: path.resolve("src/hooks"),
      locales: path.resolve("src/locales"),
      types: path.resolve("src/types"),
      utils: path.resolve("src/utils.js"),
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
