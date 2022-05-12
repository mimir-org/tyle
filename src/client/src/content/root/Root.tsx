import { QueryClient, QueryClientProvider } from "react-query";
import { GlobalStyle, themeBuilder, TyleThemeProvider } from "../../complib/core";
import { ReactQueryDevtools } from "react-query/devtools";
import { App } from "../app";
import { BrowserRouter } from "react-router-dom";
import { isProduction } from "../../models/Config";
import { usePrefersTheme } from "../../hooks/usePrefersTheme";

export const Root = () => {
  const queryClient = new QueryClient();
  const [colorTheme] = usePrefersTheme("light");

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
