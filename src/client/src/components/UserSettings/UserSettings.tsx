import SettingsSection from "components/SettingsSection";
import { useTranslation } from "react-i18next";
import UserSettingsForm from "./UserSettingsForm";

const UserSettings = () => {
  const { t } = useTranslation("settings");

  return (
    <SettingsSection title={t("usersettings.title")}>
      <UserSettingsForm />
    </SettingsSection>
  );
};

export default UserSettings;
