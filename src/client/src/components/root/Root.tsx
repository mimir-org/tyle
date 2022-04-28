import { QueryClient, QueryClientProvider } from "react-query";
import { GlobalStyle } from "../../complib/core";
import { ReactQueryDevtools } from "react-query/devtools";
import { App } from "../app";
import { BrowserRouter } from "react-router-dom";
import { isProduction } from "../../models/Config";
import { MotionConfig } from "framer-motion";
import { ThemeProvider } from "styled-components/macro";
import { themeBuilder } from "../../complib/core/constants/theme";
import { usePrefersTheme } from "../../hooks/usePrefersTheme";

export const Root = () => {
  const queryClient = new QueryClient();
  const [colorTheme] = usePrefersTheme("light");

  return (
    <BrowserRouter>
      <QueryClientProvider client={queryClient}>
        <MotionConfig reducedMotion="user">
          <ThemeProvider theme={themeBuilder({ colorTheme })}>
            <GlobalStyle />
            <App />
          </ThemeProvider>
        </MotionConfig>
        {!isProduction && <ReactQueryDevtools />}
      </QueryClientProvider>
    </BrowserRouter>
  );
};
