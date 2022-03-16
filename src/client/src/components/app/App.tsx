import { useGetCurrentUser } from "../../data/queries/auth/queriesUser";
import { Unauthenticated } from "./components/unauthenticated/Unauthenticated";
import { Authenticated } from "./components/authenticated/Authenticated";
import { FullPageSpinner } from "../../compLibrary/spinner";
import { TextResources } from "../../assets/text";

export const App = () => {
  const { data: user, isSuccess, isLoading } = useGetCurrentUser();
  const isLoggedIn = isSuccess && user;

  if (isLoading) {
    return <FullPageSpinner text={TextResources.Global_Application_Load} />;
  }

  return isLoggedIn ? <Authenticated /> : <Unauthenticated />;
};
