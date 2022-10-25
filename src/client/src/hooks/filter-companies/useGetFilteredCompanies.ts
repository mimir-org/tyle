import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useGetCompanies } from "../../data/queries/auth/queriesCompany";
import { useGetCurrentUser } from "../../data/queries/auth/queriesUser";
import { filterCompanyList } from "./hasAccess";

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
