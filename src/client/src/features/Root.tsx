import { StrictMode } from "react";
import { QueryClientProvider } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";
import { GlobalStyle, themeBuilder, TyleThemeProvider } from "../complib/core";
import { queryClient } from "../data/queries/queryClient";
import { App } from "./ui/App";
import { usePrefersTheme } from "../hooks/usePrefersTheme";
import { isProduction } from "../models/Config";

export const Root = () => {
  const [colorTheme] = usePrefersTheme("light");

  return (
    <QueryClientProvider client={queryClient}>
      <TyleThemeProvider theme={themeBuilder(colorTheme)}>
        <GlobalStyle />
        <StrictMode>
          <App />
        </StrictMode>
      </TyleThemeProvider>
      {!isProduction && <ReactQueryDevtools />}
    </QueryClientProvider>
  );
};
