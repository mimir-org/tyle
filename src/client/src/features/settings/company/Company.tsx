import { SettingsSection } from "../common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";
import { CompanyForm } from "features/settings/company/CompanyForm";
import { Box } from "complib/layouts";

export const Company = () => {
  const { t } = useTranslation("settings");

  return (
    <Box minWidth={"60%"}>
      <SettingsSection title={t("createCompany.title")}>
        <CompanyForm />
      </SettingsSection>
    </Box>
  );
};
