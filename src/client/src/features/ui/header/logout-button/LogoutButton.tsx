import { Logout } from "@styled-icons/heroicons-outline";
import { useLogout } from "external/sources/authenticate/authenticate.queries";
import { useTranslation } from "react-i18next";
import { UserMenuButton } from "../user-menu/UserMenuButton";

export const LogoutButton = () => {
  const { t } = useTranslation();
  const mutation = useLogout();

  return (
    <UserMenuButton icon={<Logout size={24} />} onClick={() => mutation.mutate()}>
      {t("user.menu.logout.title")}
    </UserMenuButton>
  );
};
