import { useTranslation } from "react-i18next";
import { FullPageSpinner } from "../../complib/feedback";
import { useGetCurrentUser } from "../../data/queries/auth/queriesUser";
import { Authenticated } from "./components/authenticated/Authenticated";
import { Unauthenticated } from "./components/unauthenticated/Unauthenticated";

export const App = () => {
  const { t } = useTranslation();
  const { data: user, isSuccess, isLoading } = useGetCurrentUser();
  const isLoggedIn = isSuccess && user;

  if (isLoading) {
    return <FullPageSpinner text={t("global.loading")} />;
  }

  return isLoggedIn ? <Authenticated /> : <Unauthenticated />;
};
