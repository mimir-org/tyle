import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { getOptionsFromEnum, Option } from "common/utils/getOptionsFromEnum";
import { useEffect } from "react";

export const useCompanyOptions = (): Option<string>[] => {
  const companies = useGetFilteredCompanies(MimirorgPermission.Manage);

  return companies.map((x) => ({
    value: String(x.id),
    label: x.displayName,
  }));
};

export const useDefaultCompanyOptions = (
  companyOptions: Option<string>[],
  selectedCompany: string,
  setSelectedCompany: (value: string) => void
) => {
  useEffect(() => {
    if (!selectedCompany && companyOptions && companyOptions.length > 0) {
      setSelectedCompany(companyOptions[0].value);
    }
  }, [companyOptions, selectedCompany, setSelectedCompany]);
};

export const getPermissionOptions = (): Option<string>[] => {
  const regularPermissions = getOptionsFromEnum<MimirorgPermission>(MimirorgPermission);
  const regularPermissionsMapped = regularPermissions.map((x) => ({
    value: String(x.value),
    label: x.label,
  }));

  return [{ value: "-1", label: "All" }, ...regularPermissionsMapped];
};
