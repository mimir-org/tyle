import { SettingsSection } from "../SettingsSection/SettingsSection";
import { useTranslation } from "react-i18next";
import { UserSettingsForm } from "components/UserSettings/UserSettingsForm";

export const UserSettings = () => {
  const { t } = useTranslation("settings");

  return (
    <SettingsSection title={t("usersettings.title")}>
      <UserSettingsForm />
    </SettingsSection>
  );
};
