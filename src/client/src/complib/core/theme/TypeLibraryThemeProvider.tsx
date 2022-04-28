import { PropsWithChildren } from "react";
import { ThemeProvider } from "styled-components/macro";
import { TypeLibraryTheme } from "./theme";

interface TypeLibraryThemeProviderProps {
  theme: TypeLibraryTheme;
}

export const TypeLibraryThemeProvider = ({ theme, children }: PropsWithChildren<TypeLibraryThemeProviderProps>) => {
  const customTheme = {
    typeLibrary: theme,
  };

  return <ThemeProvider theme={customTheme}>{children}</ThemeProvider>;
};
