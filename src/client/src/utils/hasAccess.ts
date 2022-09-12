import { MimirorgCompanyCm, MimirorgPermission, MimirorgUserCm } from "@mimirorg/typelibrary-types";

/**
 * Check if user has access to a spesific company with given permission.
 * @param {MimirorgUserCm}  user - The user you eant to check for access.
 * @param {number} company - The company id you want to check access against.
 * @param {MimirorgPermission} [permission] - The permission enum you want to check access for.
 * @returns {boolean} Returns true if user has access
 */
export const hasAccess = (user: MimirorgUserCm, company: number, permission: MimirorgPermission): boolean => {
  if (!user) return false;
  if (company < 1) return false;
  const companyPermission = user?.permissions[company];

  return (companyPermission & permission) === permission;
};

/**
 * Filter a company list for a given user and a given permission.
 * @param {MimirorgCompanyCm[]} companies - The company list you want to filter.
 * @param {MimirorgUserCm}  user - The user you eant to check for access.
 * @param {MimirorgPermission} [permission] - The permission enum you want to check access for.
 * @returns {MimirorgCompanyCm[]} Returns a filtered list of companies
 */
export const filterCompanyList = (
  companies: MimirorgCompanyCm[],
  user: MimirorgUserCm,
  permission: MimirorgPermission
): MimirorgCompanyCm[] => {
  if (!user || !companies || !companies.some((x) => x)) return [] as MimirorgCompanyCm[];
  return companies.filter((x) => hasAccess(user, x.id, permission));
};
