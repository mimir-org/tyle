import { QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { queryClient } from "api/clients/queryClient";
import App from "components/App";
import { isProduction } from "config";
import { StrictMode } from "react";

const Root = () => (
  <QueryClientProvider client={queryClient}>
    <ThemeProvider theme={"tyleLight"}>
      <StrictMode>
        <App />
      </StrictMode>
    </ThemeProvider>
    {!isProduction && <ReactQueryDevtools />}
  </QueryClientProvider>
);

export default Root;
