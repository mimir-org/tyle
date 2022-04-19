import { QueryClient, QueryClientProvider } from "react-query";
import { GlobalStyle } from "../../complib/core";
import { ReactQueryDevtools } from "react-query/devtools";
import { App } from "../app";
import { BrowserRouter } from "react-router-dom";
import { isProduction } from "../../models/Config";
import { MotionConfig } from "framer-motion";

export const Root = () => {
  const queryClient = new QueryClient();

  return (
    <BrowserRouter>
      <QueryClientProvider client={queryClient}>
        <MotionConfig reducedMotion="user">
          <GlobalStyle />
          <App />
        </MotionConfig>
        {!isProduction && <ReactQueryDevtools />}
      </QueryClientProvider>
    </BrowserRouter>
  );
};
