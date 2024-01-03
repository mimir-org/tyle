import { useGetCurrentUser } from "api/user.queries";
import Authenticated from "components/Authenticated";
import FullPageSpinner from "components/FullPageSpinner";
import Unauthenticated from "components/Unauthenticated";

const App = () => {
  const { data: user, isSuccess, isLoading } = useGetCurrentUser();
  const isLoggedIn = isSuccess && user;

  if (isLoading) {
    return <FullPageSpinner text="Loading application" />;
  }

  return isLoggedIn ? <Authenticated /> : <Unauthenticated />;
};

export default App;
