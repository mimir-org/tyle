import { PropsWithChildren } from "react";
import { ThemeProvider } from "styled-components/macro";
import { TyleTheme } from "./theme";
import { MotionConfig } from "framer-motion";

interface TyleThemeProviderProps {
  theme: TyleTheme;
}

/**
 * Custom theme provider which exposes a namespaced theme for the Tyle.
 * Also wraps its children with other global dependencies that the Tyle components are dependent upon.
 *
 * @param theme
 * @param children
 * @constructor
 */
export const TyleThemeProvider = ({ theme, children }: PropsWithChildren<TyleThemeProviderProps>) => {
  const customTheme = {
    tyle: theme,
  };

  return (
    <ThemeProvider theme={customTheme}>
      <MotionConfig reducedMotion="user">{children}</MotionConfig>
    </ThemeProvider>
  );
};
