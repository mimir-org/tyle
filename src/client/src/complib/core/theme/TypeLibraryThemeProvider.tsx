import { PropsWithChildren } from "react";
import { ThemeProvider } from "styled-components/macro";
import { TypeLibraryTheme } from "./theme";
import { MotionConfig } from "framer-motion";

interface TypeLibraryThemeProviderProps {
  theme: TypeLibraryTheme;
}

/**
 * Custom theme provider which exposes a namespaced theme for the 'Type library'.
 * Also wraps its children with other global dependencies which the associated Type library components are dependent upon.
 *
 * @param theme
 * @param children
 * @constructor
 */
export const TypeLibraryThemeProvider = ({ theme, children }: PropsWithChildren<TypeLibraryThemeProviderProps>) => {
  const customTheme = {
    typeLibrary: theme,
  };

  return (
    <ThemeProvider theme={customTheme}>
      <MotionConfig reducedMotion="user">{children}</MotionConfig>
    </ThemeProvider>
  );
};
