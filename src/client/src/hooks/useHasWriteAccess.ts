import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useGetCurrentUser } from "api/user.queries";
import { UserItem } from "types/userItem";

/**
 * Returns true if the current user has write access
 * @returns {boolean} True if the current user has write access
 * @example
 * const hasWriteAccess = useHasWriteAccess();
 * // hasWriteAccess = true
 */
export const useHasWriteAccess = (): boolean => {
  const userQuery = useGetCurrentUser();
  const permissionForCompany = userQuery.data?.permissions[0] ?? 0;

  return (permissionForCompany & MimirorgPermission.Write) === MimirorgPermission.Write;
};

/**
 * Returns true if the current user has write access
 * @param currentUser
 * @returns {boolean} True if the current user has write access
 * @example
 * const hasWriteAccess = useHasWriteAccess();
 * // hasWriteAccess = true
 */

export const hasWriteAccess = (currentUser: UserItem): boolean => {
  const permissionForCompany = currentUser.permissions[0].value ?? 0;

  return (permissionForCompany & MimirorgPermission.Write) === MimirorgPermission.Write;
};
