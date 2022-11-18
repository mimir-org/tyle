import { SettingsContainer } from "features/settings/layout/SettingsLayout.styled";
import { Sidebar } from "features/settings/sidebar/Sidebar";
import { useTranslation } from "react-i18next";
import { Outlet } from "react-router-dom";

export const SettingsLayout = () => {
  const { t } = useTranslation();

  return (
    <SettingsContainer>
      <Sidebar title={t("settings.title")} groups={[]} />
      <Outlet />
    </SettingsContainer>
  );
};
