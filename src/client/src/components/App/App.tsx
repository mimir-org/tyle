import { FullPageSpinner } from "@mimirorg/component-library";
import { useGetCurrentUser } from "api/user.queries";
import Authenticated from "components/Authenticated";
import Unauthenticated from "components/Unauthenticated";
import { useTranslation } from "react-i18next";

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
