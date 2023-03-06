import { SettingsSection } from "../common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";

export const CreateCompany = () => {
  const { t } = useTranslation("settings");

  return (
    <SettingsSection title={t("createCompany.title")}>
      
    </SettingsSection>
  );
};
