import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useGetAuthCompanyUsers } from "api/company.queries";
import { UserItem } from "common/types/userItem";
import { getOptionsFromEnum, Option } from "common/utils/getOptionsFromEnum";
import { mapMimirorgUserCmToUserItem } from "common/utils/mappers/mapMimirorgUserCmToUserItem";
import { MimirorgPermissionExtended, UserItemPermission } from "components/Permissions/userItemPermission";
import { useGetFilteredCompanies } from "hooks/useGetFilteredCompanies";
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
  setSelectedCompany: (value: string) => void,
) => {
  useEffect(() => {
    if (!selectedCompany && companyOptions && companyOptions.length > 0) {
      setSelectedCompany(companyOptions[0].value);
    }
  }, [companyOptions, selectedCompany, setSelectedCompany]);
};

/**
 * Returns the permission options for the permission dropdown.
 * The options are based on the MimirorgPermission enum.
 * @returns {Option<string>[]} The permission options
 * @example
 * const permissionOptions = getPermissionOptions();
 * // permissionOptions = [
 * //   { value: "-1", label: "All" },
 * //   { value: "0", label: "None" },
 * //   { value: "1", label: "Read" },
 * //   { value: "2", label: "Write" },
 * //   { value: "3", label: "Manage" },
 * // ];
 */
export const getPermissionOptions = (): Option<string>[] => {
  const regularPermissions = getOptionsFromEnum<MimirorgPermission>(MimirorgPermission);
  const regularPermissionsMapped = regularPermissions.map((x) => ({
    value: String(x.value),
    label: x.label,
  }));

  return [{ value: "-1", label: "All" }, ...regularPermissionsMapped];
};

export const useFilteredUsers = (companyId: string, permission: UserItemPermission): UserItem[] => {
  const userQuery = useGetAuthCompanyUsers(companyId);
  const users = userQuery.data?.map((x) => mapMimirorgUserCmToUserItem(x)) ?? [];

  if (permission === MimirorgPermissionExtended.All) {
    return users.filter((user) => user.permissions[companyId]);
  }

  return users.filter((user) => user.permissions[companyId]?.value === permission);
};
