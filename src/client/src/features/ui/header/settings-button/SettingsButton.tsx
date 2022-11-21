import { PopoverClose } from "@radix-ui/react-popover";
import { Cog } from "@styled-icons/heroicons-outline";
import { PlainLink } from "common/components/plain-link";
import { settingsBasePath } from "features/settings/SettingsRoutes";
import { UserMenuButton } from "features/ui/header/user-menu/UserMenuButton";
import { useTranslation } from "react-i18next";

export const SettingsButton = () => {
  const { t } = useTranslation();

  return (
    <PopoverClose asChild>
      <PlainLink tabIndex={-1} to={settingsBasePath}>
        <UserMenuButton tabIndex={0} icon={<Cog size={24} />}>
          {t("user.menu.settings.title")}
        </UserMenuButton>
      </PlainLink>
    </PopoverClose>
  );
};
