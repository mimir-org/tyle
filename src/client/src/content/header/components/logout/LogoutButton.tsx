import { Logout } from "@styled-icons/heroicons-outline";
import { useTranslation } from "react-i18next";
import { useLogout } from "../../../../data/queries/auth/queriesAuthenticate";
import { UserMenuButton } from "../menu/UserMenuButton";

export const LogoutButton = () => {
  const { t } = useTranslation();
  const mutation = useLogout();

  return (
    <UserMenuButton icon={<Logout size={24} />} onClick={() => mutation.mutate()}>
      {t("user.menu.logout.title")}
    </UserMenuButton>
  );
};
