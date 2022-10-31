import { TooltipProvider } from "@radix-ui/react-tooltip";
import { TyleTheme } from "complib/core/theme/theme";
import { Toaster } from "complib/data-display";
import { MotionConfig } from "framer-motion";
import { PropsWithChildren } from "react";
import { ThemeProvider } from "styled-components/macro";

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
      <MotionConfig reducedMotion="user">
        <TooltipProvider>{children}</TooltipProvider>
        <Toaster />
      </MotionConfig>
    </ThemeProvider>
  );
};
