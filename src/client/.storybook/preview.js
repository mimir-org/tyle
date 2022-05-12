import { GlobalStyle, themeBuilder, TyleThemeProvider } from "../src/complib/core";

export const parameters = {
  actions: { argTypesRegex: "^on[A-Z].*" },
  controls: {
    matchers: {
      color: /(background|color)$/i,
      date: /Date$/,
    },
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
