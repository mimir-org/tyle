import { GlobalStyle } from "../../compLibrary/GlobalStyle";
import { LoginBox } from "./styled/LoginBox";
import { TextResources } from "../../assets/text";
import { msalInstance } from "../../index";
import { Home } from "../home";
import { Button } from "../../compLibrary/buttons";
import { IPublicClientApplication } from "@azure/msal-browser";
import { QueryClient, QueryClientProvider } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";
import { AuthenticatedTemplate, MsalProvider, UnauthenticatedTemplate } from "@azure/msal-react";

type AppProps = {
  pca: IPublicClientApplication;
};

export const App = ({ pca }: AppProps) => {
  const login = () => msalInstance.loginRedirect();
  const queryClient = new QueryClient();

  return (
    <MsalProvider instance={pca}>
      <AuthenticatedTemplate>
        <GlobalStyle />
        <QueryClientProvider client={queryClient}>
          <Home />
          <ReactQueryDevtools />
        </QueryClientProvider>
      </AuthenticatedTemplate>
      <UnauthenticatedTemplate>
        <LoginBox>
          <Button text={TextResources.Login_Label} onClick={login} />
        </LoginBox>
      </UnauthenticatedTemplate>
    </MsalProvider>
  );
};
