import { Box } from "@mimirorg/component-library";
import SettingsSection from "components/SettingsSection";
import { useTranslation } from "react-i18next";
import CreateCompanyForm from "./CreateCompanyForm";
import UpdateCompanyForm from "./UpdateCompanyForm";

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
