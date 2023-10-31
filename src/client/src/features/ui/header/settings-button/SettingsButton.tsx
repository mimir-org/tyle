import { PopoverClose } from "@radix-ui/react-popover";
import { Cog } from "@styled-icons/heroicons-outline";
import { PlainLink } from "components/PlainLink";
import { settingsBasePath } from "components/Settings/SettingsRoutes";
import { UserMenuButton } from "features/ui/header/user-menu/UserMenuButton";
import { useTranslation } from "react-i18next";

export const SettingsButton = () => {
  const { t } = useTranslation("ui");

  return (
    <PopoverClose asChild>
      <PlainLink tabIndex={-1} to={settingsBasePath}>
        <UserMenuButton tabIndex={0} icon={<Cog size={24} />}>
          {t("header.menu.settings.title")}
        </UserMenuButton>
      </PlainLink>
    </PopoverClose>
  );
};
