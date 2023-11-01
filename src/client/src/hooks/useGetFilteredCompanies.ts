import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useGetCompanies } from "api/company.queries";
import { useGetCurrentUser } from "api/user.queries";
import { filterCompanyList } from "helpers/access.helpers";

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
