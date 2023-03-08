import { SettingsSection } from "../common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";
import { UserSettingsForm } from "features/settings/usersettings/UserSettingsForm";

export const UserSettings = () => {
  const { t } = useTranslation("settings");

  return (
    <SettingsSection title={t("usersettings.title")}>
      <UserSettingsForm />
    </SettingsSection>
  );
};