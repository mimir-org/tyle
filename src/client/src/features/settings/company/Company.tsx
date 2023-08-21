import { SettingsSection } from "../common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";
import { CreateCompanyForm } from "features/settings/company/CreateCompanyForm";
import { UpdateCompanyForm } from "features/settings/company/UpdateCompanyForm";
import { Box } from "@mimirorg/component-library";

export type CompanyProps = {
  update?: boolean;
};

export const Company = ({ update }: CompanyProps) => {
  const { t } = useTranslation("settings");

  return (
    <Box minWidth={"60%"}>
      <SettingsSection title={update ? t("company.title.update") : t("company.title.create")}>
        {update ? <UpdateCompanyForm /> : <CreateCompanyForm />}
      </SettingsSection>
    </Box>
  );
};
