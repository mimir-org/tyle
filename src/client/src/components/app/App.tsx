import { useGetCurrentUser } from "../../data/queries/auth/queriesUser";
import { Unauthenticated } from "./components/unauthenticated/Unauthenticated";
import { Authenticated } from "./components/authenticated/Authenticated";
import { FullPageSpinner } from "../../complib/feedback";
import { TextResources } from "../../assets/text";

export const App = () => {
  const { data: user, isSuccess, isLoading } = useGetCurrentUser();
  const isLoggedIn = isSuccess && user;

  if (isLoading) {
    return <FullPageSpinner text={TextResources.GLOBAL_APPLICATION_LOAD} />;
  }

  return isLoggedIn ? <Authenticated /> : <Unauthenticated />;
};
