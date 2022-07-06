import { DocsContainer } from "@storybook/addon-docs/blocks";
import { GlobalStyle, themeBuilder, TyleThemeProvider } from "../src/complib/core";

export const parameters = {
  actions: { argTypesRegex: "^on[A-Z].*" },
  controls: {
    matchers: {
      color: /(background|color)$/i,
      date: /Date$/,
    },
  },
  docs: {
    container: ({ children, context }) => (
      <TyleThemeProvider theme={themeBuilder("light")}>
        <DocsContainer context={context}>{children}</DocsContainer>
      </TyleThemeProvider>
    ),
  },
  backgrounds: {
    values: [
      {
        name: "full white",
        value: "#ffffff",
      },
      {
        name: "full dark",
        value: "#000000",
      },
      {
        name: "light background",
        value: "#fdfbff",
      },
      {
        name: "light surface",
        value: "#f7f6ff",
      },
      {
        name: "dark background",
        value: "#13131c",
      },
      {
        name: "dark surface",
        value: "#32333c",
      },
    ],
  },
};

export const decorators = [
  (Story) => (
    <TyleThemeProvider theme={themeBuilder("light")}>
      <GlobalStyle />
      <Story />
    </TyleThemeProvider>
  ),
];
