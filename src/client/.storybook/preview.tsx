import { DocsContainer } from "@storybook/addon-docs";
import { GlobalStyle, themeBuilder, TyleThemeProvider } from "../src/complib/core";
import { Preview } from "@storybook/react";

interface SBContainerProps {
  context: any;
  children: React.ReactNode;
}

const preview: Preview = {
  decorators: [
    (Story) => (
      <TyleThemeProvider theme={themeBuilder("light")}>
        <GlobalStyle />
        <Story />
      </TyleThemeProvider>
    ),
  ],
  parameters: {
    actions: { argTypesRegex: "^on[A-Z].*" },
    controls: {
      matchers: {
        color: /(background|color)$/i,
        date: /Date$/,
      },
    },
    docs: {
      container: ({ children, context }: SBContainerProps) => (
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
  },
};

export default preview;
