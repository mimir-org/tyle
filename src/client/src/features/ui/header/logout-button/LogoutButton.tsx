import { ArrowLeftOnRectangle } from "@styled-icons/heroicons-outline";
import { useLogout } from "external/sources/authenticate/authenticate.queries";
import { UserMenuButton } from "features/ui/header/user-menu/UserMenuButton";
import { useTranslation } from "react-i18next";

export const LogoutButton = () => {
  const { t } = useTranslation("ui");
  const mutation = useLogout();

  return (
    <UserMenuButton dangerousAction icon={<ArrowLeftOnRectangle size={24} />} onClick={() => mutation.mutate()}>
      {t("header.menu.logout.title")}
    </UserMenuButton>
  );
};
