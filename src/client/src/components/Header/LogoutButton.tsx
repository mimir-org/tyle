import { ArrowLeftOnRectangle } from "@styled-icons/heroicons-outline";
import { useLogout } from "api/authenticate.queries";
import { useTranslation } from "react-i18next";
import UserMenuButton from "./UserMenuButton";

const LogoutButton = () => {
  const { t } = useTranslation("ui");
  const mutation = useLogout();

  return (
    <UserMenuButton dangerousAction icon={<ArrowLeftOnRectangle size={24} />} onClick={() => mutation.mutate()}>
      {t("header.menu.logout.title")}
    </UserMenuButton>
  );
};

export default LogoutButton;
