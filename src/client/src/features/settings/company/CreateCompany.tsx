import { SettingsSection } from "../common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";
import { CreateCompanyForm } from "features/settings/company/CreateCompanyForm";
import { Box } from "complib/layouts";

export const CreateCompany = () => {
  const { t } = useTranslation("settings");

  return (
    <Box minWidth={"60%"}>
      <SettingsSection title={t("createCompany.title")}>
        <CreateCompanyForm />
      </SettingsSection>
    </Box>
  );
};
