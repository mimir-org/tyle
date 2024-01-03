import "@fontsource/roboto/300.css";
import "@fontsource/roboto/400.css";
import "@fontsource/roboto/500.css";
import "@fontsource/roboto/700.css";
import { TooltipProvider } from "@radix-ui/react-tooltip";
import Toaster from "components/Toaster";
import { MotionConfig } from "framer-motion";
import { usePrefersTheme } from "hooks/usePrefersTheme";
import { PropsWithChildren } from "react";
import { ThemeProvider } from "styled-components";
import { GlobalStyle } from "./GlobalStyle";
import { themeBuilder } from "./theme";

export interface TyleThemeProviderProps {
  theme?: "light" | "dark";
}

/**
 * Custom theme provider which exposes a namespaced theme.
 * Also wraps its children with other global dependencies that the components are dependent upon.
 *
 * @param theme
 * @param children
 * @constructor
 */
const TyleThemeProvider = ({ theme = "light", children }: PropsWithChildren<TyleThemeProviderProps>) => {
  const [colorTheme] = usePrefersTheme(theme);

  const applicationTheme = {
    tyle: themeBuilder(colorTheme),
  };

  return (
    <ThemeProvider theme={applicationTheme}>
      <GlobalStyle />
      <MotionConfig reducedMotion="user">
        <TooltipProvider>{children}</TooltipProvider>
        <Toaster />
      </MotionConfig>
    </ThemeProvider>
  );
};

export default TyleThemeProvider;
