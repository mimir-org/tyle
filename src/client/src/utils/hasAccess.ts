import { MimirorgPermission, MimirorgUserCm } from "@mimirorg/typelibrary-types";

export const enumToBitValues = (enumValue: object): number[] => Object.keys(enumValue).map(Number).filter(Boolean);

export const hasAccess = (user: MimirorgUserCm, company: number, permission: MimirorgPermission): boolean => {
  const companyPermission = user?.permissions[company] && (user?.permissions[company] as MimirorgPermission);
  if ((companyPermission & permission) === permission) return true;
  return false;
};
