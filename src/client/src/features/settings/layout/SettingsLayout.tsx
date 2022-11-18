import { useSettingsLinkGroups } from "features/settings/layout/SettingsLayout.helpers";
import { SettingsContainer } from "features/settings/layout/SettingsLayout.styled";
import { Sidebar } from "features/settings/layout/sidebar/Sidebar";
import { useTranslation } from "react-i18next";
import { Outlet } from "react-router-dom";

export const SettingsLayout = () => {
  const { t } = useTranslation();
  const groups = useSettingsLinkGroups();

  return (
    <SettingsContainer>
      <Sidebar title={t("settings.title")} groups={groups} />
      <Outlet />
    </SettingsContainer>
  );
};
