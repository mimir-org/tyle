import { usePrefersTheme } from "common/hooks/usePrefersTheme";
import { isProduction } from "common/utils/config";
import { GlobalStyle, themeBuilder, TyleThemeProvider } from "complib/core";
import { queryClient } from "external/client/queryClient";
import { App } from "features/ui/App";
import { StrictMode } from "react";
import { QueryClientProvider } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";

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
