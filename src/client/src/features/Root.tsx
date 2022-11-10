import { QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { usePrefersTheme } from "common/hooks/usePrefersTheme";
import { isProduction } from "common/utils/config";
import { GlobalStyle, themeBuilder, TyleThemeProvider } from "complib/core";
import { queryClient } from "external/client/queryClient";
import { App } from "features/ui/App";
import { StrictMode } from "react";

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
