import { FullPageSpinner } from "complib/feedback";
import { useGetCurrentUser } from "external/sources/user/user.queries";
import { Authenticated } from "features/ui/authenticated/Authenticated";
import { Unauthenticated } from "features/ui/unauthenticated/Unauthenticated";
import { useTranslation } from "react-i18next";

export const App = () => {
  const { t } = useTranslation("ui");
  const { data: user, isSuccess, isLoading } = useGetCurrentUser();
  const isLoggedIn = isSuccess && user;

  console.log(isLoggedIn);

  if (isLoading) {
    return <FullPageSpinner text={t("global.loading")} />;
  }

  return isLoggedIn ? <Authenticated /> : <Unauthenticated />;
};
