import { GlobalStyle, themeBuilder, TypeLibraryThemeProvider } from "../src/complib/core";

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
    <TypeLibraryThemeProvider theme={themeBuilder("light")}>
      <GlobalStyle />
      <Story />
    </TypeLibraryThemeProvider>
  ),
];
