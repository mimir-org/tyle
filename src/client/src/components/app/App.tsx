import { GlobalStyle } from "../../compLibrary/GlobalStyle";
import { LoginBox } from "./styled/LoginBox";
import { TextResources } from "../../assets/text";
import { msalInstance } from "../../index";
import { Home } from "../home";
import { Button } from "../../compLibrary/buttons";
import { IPublicClientApplication } from "@azure/msal-browser";
import { AuthenticatedTemplate, MsalProvider, UnauthenticatedTemplate } from "@azure/msal-react";

type AppProps = {
  pca: IPublicClientApplication;
};

export const App = ({ pca }: AppProps) => {
  const login = () => msalInstance.loginRedirect();

  return (
    <MsalProvider instance={pca}>
      <AuthenticatedTemplate>
        <GlobalStyle />
        <Home/>
      </AuthenticatedTemplate>
      <UnauthenticatedTemplate>
        <LoginBox>
          <Button text={TextResources.Login_Label} onClick={login} />
        </LoginBox>
      </UnauthenticatedTemplate>
    </MsalProvider>
  );
};