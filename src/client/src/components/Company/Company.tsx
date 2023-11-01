import { SettingsSection } from "../SettingsSection/SettingsSection";
import { useTranslation } from "react-i18next";
import { CreateCompanyForm } from "components/Company/CreateCompanyForm";
import { UpdateCompanyForm } from "components/Company/UpdateCompanyForm";
import { Box } from "@mimirorg/component-library";

export type CompanyProps = {
  update?: boolean;
};

const Company = ({ update }: CompanyProps) => {
  const { t } = useTranslation("settings");

  return (
    <Box minWidth={"60%"}>
      <SettingsSection title={update ? t("company.title.update") : t("company.title.create")}>
        {update ? <UpdateCompanyForm /> : <CreateCompanyForm />}
      </SettingsSection>
    </Box>
  );
};

export default Company;