import { GlobalStyle, themeBuilder, TyleThemeProvider } from "complib/core";
import { StrictMode } from "react";
import { QueryClientProvider } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";
import { isProduction } from "../common/utils/config";
import { queryClient } from "../data/queries/queryClient";
import { usePrefersTheme } from "../hooks/usePrefersTheme";
import { App } from "./ui/App";

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
