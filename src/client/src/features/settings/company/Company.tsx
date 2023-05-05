import { SettingsSection } from "../common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";
import { CompanyForm } from "features/settings/company/CompanyForm";
import { Box, Flexbox } from "complib/layouts";
import { useTheme } from "styled-components";
import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { Option } from "common/utils/getOptionsFromEnum";
import { RadioFilters } from "../common/radio-filters/RadioFilters";
import { useEffect, useState } from "react";
import { createEmptyFormMimirorgCompany, mapCompanyCmToFormCompany } from "./CompanyForm.helpers";

export const Company = () => {
  const theme = useTheme();
  const { t } = useTranslation("settings");

  const companies = useGetFilteredCompanies(MimirorgPermission.Manage);
  const companyOptions = companies.map(x => ({ value: String(x.id), label: x.displayName })) as Option<string>[];
  companyOptions.unshift({ value: "0", label: "Create new company" });
  const [selectedCompany, setSelectedCompany] = useState(companyOptions[0]?.value);
  const [defaultValues, setDefaultValues] = useState(createEmptyFormMimirorgCompany());

  useEffect(() => {
    setDefaultValues(selectedCompany == "0" ? createEmptyFormMimirorgCompany(): mapCompanyCmToFormCompany(companies.find(x => x.id == Number(selectedCompany))))
  }, [selectedCompany, companies]);
  
  return (
    <Box minWidth={"60%"}>
      <SettingsSection title={t("createCompany.title")}>
        <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xxl}>
          <RadioFilters
            filters={companyOptions}
            value={selectedCompany}
            onChange={(x) => setSelectedCompany(x) }
          />
          <CompanyForm defaultValues={defaultValues} companyId={selectedCompany} />
        </Flexbox>
      </SettingsSection>
    </Box>
  );
};
