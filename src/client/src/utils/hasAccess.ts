import { MimirorgPermission, MimirorgUserCm } from "@mimirorg/typelibrary-types";

export const hasAccess = (user: MimirorgUserCm, company: number, permission: MimirorgPermission): boolean => {
  if (!user) return false;
  if (company < 1) return false;
  const companyPermission = user?.permissions[company] && (user?.permissions[company] as MimirorgPermission);
  if ((companyPermission & permission) === permission) return true;
  return false;
};
