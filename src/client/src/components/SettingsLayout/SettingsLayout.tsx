import { Outlet } from "react-router-dom";
import { useSettingsLinkGroups } from "./SettingsLayout.helpers";
import SettingsContainer from "./SettingsLayout.styled";
import Sidebar from "./Sidebar";

const SettingsLayout = () => {
  const groups = useSettingsLinkGroups();

  return (
    <SettingsContainer>
      <Sidebar title="Settings" groups={groups} />
      <Outlet />
    </SettingsContainer>
  );
};

export default SettingsLayout;
