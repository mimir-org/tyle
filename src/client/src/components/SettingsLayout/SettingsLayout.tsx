import { useSettingsLinkGroups } from "components/SettingsLayout/SettingsLayout.helpers";
import { SettingsContainer } from "components/SettingsLayout/SettingsLayout.styled";
import { Sidebar } from "components/SettingsLayout/Sidebar";
import { useTranslation } from "react-i18next";
import { Outlet } from "react-router-dom";

const SettingsLayout = () => {
  const { t } = useTranslation("settings");
  const groups = useSettingsLinkGroups();

  return (
    <SettingsContainer>
      <Sidebar title={t("title")} groups={groups} />
      <Outlet />
    </SettingsContainer>
  );
};

export default SettingsLayout;