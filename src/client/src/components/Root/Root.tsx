import { QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { queryClient } from "api/clients/queryClient";
import App from "components/App";
import TyleThemeProvider from "components/TyleThemeProvider";
import { isProduction } from "config";
import { StrictMode } from "react";

const Root = () => (
  <QueryClientProvider client={queryClient}>
    <TyleThemeProvider theme={"light"}>
      <StrictMode>
        <App />
      </StrictMode>
    </TyleThemeProvider>
    {!isProduction && <ReactQueryDevtools />}
  </QueryClientProvider>
);

export default Root;
