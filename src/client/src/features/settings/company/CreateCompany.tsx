import { SettingsSection } from "../common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";
import { CreateCompanyForm } from "features/settings/company/CreateCompanyForm";

export const CreateCompany = () => {
  const { t } = useTranslation("settings");

  return (
    <SettingsSection title={t("createCompany.title")}>
      <CreateCompanyForm />
    </SettingsSection>
  );
};
