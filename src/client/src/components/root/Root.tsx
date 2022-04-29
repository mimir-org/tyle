import { QueryClient, QueryClientProvider } from "react-query";
import { GlobalStyle, themeBuilder, TypeLibraryThemeProvider } from "../../complib/core";
import { ReactQueryDevtools } from "react-query/devtools";
import { App } from "../app";
import { BrowserRouter } from "react-router-dom";
import { isProduction } from "../../models/Config";
import { MotionConfig } from "framer-motion";
import { usePrefersTheme } from "../../hooks/usePrefersTheme";

export const Root = () => {
  const queryClient = new QueryClient();
  const [colorTheme] = usePrefersTheme("light");

  return (
    <BrowserRouter>
      <QueryClientProvider client={queryClient}>
        <MotionConfig reducedMotion="user">
          <TypeLibraryThemeProvider theme={themeBuilder(colorTheme)}>
            <GlobalStyle />
            <App />
          </TypeLibraryThemeProvider>
        </MotionConfig>
        {!isProduction && <ReactQueryDevtools />}
      </QueryClientProvider>
    </BrowserRouter>
  );
};
