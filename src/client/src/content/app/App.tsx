import { useGetCurrentUser } from "../../data/queries/auth/queriesUser";
import { FullPageSpinner } from "../../complib/feedback";
import { TextResources } from "../../assets/text";
import { Authenticated } from "./components/authenticated/Authenticated";
import { Unauthenticated } from "./components/unauthenticated/Unauthenticated";

export const App = () => {
  const { data: user, isSuccess, isLoading } = useGetCurrentUser();
  const isLoggedIn = isSuccess && user;

  if (isLoading) {
    return <FullPageSpinner text={TextResources.GLOBAL_APPLICATION_LOAD} />;
  }

  return isLoggedIn ? <Authenticated /> : <Unauthenticated />;
};
