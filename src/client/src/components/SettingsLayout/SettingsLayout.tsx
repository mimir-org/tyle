import { useTranslation } from "react-i18next";
import { Outlet } from "react-router-dom";
import { useSettingsLinkGroups } from "./SettingsLayout.helpers";
import SettingsContainer from "./SettingsLayout.styled";
import Sidebar from "./Sidebar";

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
