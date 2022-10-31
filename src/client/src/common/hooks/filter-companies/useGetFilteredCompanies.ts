import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { filterCompanyList } from "common/hooks/filter-companies/hasAccess";
import { useGetCompanies } from "external/sources/company/company.queries";
import { useGetCurrentUser } from "external/sources/user/user.queries";

/**
 * Returns companies available for the current user given a certain permission level
 *
 * @param permissionLevel
 */
export const useGetFilteredCompanies = (permissionLevel: MimirorgPermission) => {
  const userQuery = useGetCurrentUser();
  const companyQuery = useGetCompanies();
  return filterCompanyList(companyQuery.data, userQuery.data, permissionLevel);
};
