import { FullPageSpinner } from "@mimirorg/component-library";
import { useGetCurrentUser } from "api/user.queries";
import { useTranslation } from "react-i18next";
import Authenticated from "../Authenticated";
import Unauthenticated from "../Unauthenticated";

const App = () => {
  const { t } = useTranslation("ui");
  const { data: user, isSuccess, isLoading } = useGetCurrentUser();
  const isLoggedIn = isSuccess && user;

  if (isLoading) {
    return <FullPageSpinner text={t("global.loading")} />;
  }

  return isLoggedIn ? <Authenticated /> : <Unauthenticated />;
};

export default App;
