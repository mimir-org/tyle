import { QueryClient, QueryClientProvider } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";
import { BrowserRouter } from "react-router-dom";
import { GlobalStyle, themeBuilder, TyleThemeProvider } from "../../complib/core";
import { usePrefersTheme } from "../../hooks/usePrefersTheme";
import { isProduction } from "../../models/Config";
import { App } from "../app";

export const Root = () => {
  const queryClient = new QueryClient();
  const [colorTheme] = usePrefersTheme("light", { storageOnly: true });

  return (
    <BrowserRouter>
      <QueryClientProvider client={queryClient}>
        <TyleThemeProvider theme={themeBuilder(colorTheme)}>
          <GlobalStyle />
          <App />
        </TyleThemeProvider>
        {!isProduction && <ReactQueryDevtools />}
      </QueryClientProvider>
    </BrowserRouter>
  );
};
