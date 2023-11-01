import { MimirorgThemeProvider } from "@mimirorg/component-library";
import { QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { queryClient } from "api/clients/queryClient";
import App from "components/App";
import { isProduction } from "config";
import { StrictMode } from "react";

const Root = () => (
  <QueryClientProvider client={queryClient}>
    <MimirorgThemeProvider theme={"tyleLight"}>
      <StrictMode>
        <App />
      </StrictMode>
    </MimirorgThemeProvider>
    {!isProduction && <ReactQueryDevtools />}
  </QueryClientProvider>
);

export default Root;
