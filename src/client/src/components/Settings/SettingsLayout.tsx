import { useSettingsLinkGroups } from "components/Settings/SettingsLayout.helpers";
import { SettingsContainer } from "components/Settings/SettingsLayout.styled";
import { Sidebar } from "components/Settings/Sidebar";
import { useTranslation } from "react-i18next";
import { Outlet } from "react-router-dom";

export const SettingsLayout = () => {
  const { t } = useTranslation("settings");
  const groups = useSettingsLinkGroups();

  return (
    <SettingsContainer>
      <Sidebar title={t("title")} groups={groups} />
      <Outlet />
    </SettingsContainer>
  );
};
