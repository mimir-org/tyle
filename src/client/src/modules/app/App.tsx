import { Home } from "../home/";
import { GlobalStyle } from "../../compLibrary";
import { useAppSelector, isFetchingSelector } from "../../redux/store";
import { LoginBox } from "./styled";
import { TextResources } from "../../assets/text";
import { useDispatch } from "react-redux";
import { IPublicClientApplication } from "@azure/msal-browser";
import { msalInstance } from "../..";
import { AuthenticatedTemplate, MsalProvider, UnauthenticatedTemplate } from "@azure/msal-react";
import { Button } from "../../compLibrary/buttons";
import { Spinner, SpinnerWrapper } from "../../compLibrary/animated";

type AppProps = {
  pca: IPublicClientApplication;
};

const App = ({ pca }: AppProps) => {
  const isFetching = useAppSelector(isFetchingSelector);

  const login = () => msalInstance.loginRedirect();
  const dispatch = useDispatch();

  return (
    <MsalProvider instance={pca}>
      <AuthenticatedTemplate>
        <GlobalStyle />
        <SpinnerWrapper fetching={isFetching}>
          <Spinner />
        </SpinnerWrapper>
        <Home dispatch={dispatch} />
      </AuthenticatedTemplate>
      <UnauthenticatedTemplate>
        <LoginBox>
          <Button text={TextResources.Login_Label} onClick={login} />
        </LoginBox>
      </UnauthenticatedTemplate>
    </MsalProvider>
  );
};

export default App;
