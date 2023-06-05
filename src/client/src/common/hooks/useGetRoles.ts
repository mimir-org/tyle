import { useGetCurrentUser } from "external/sources/user/user.queries";

/**
 * Returns roles for the current user
 *
 */
export const useGetRoles = () => {
  const userQuery = useGetCurrentUser();
  return userQuery.data?.roles;
};

/**
 * Returns true if the current user has write access
 * @returns {boolean} True if the current user has write access
 * @example
 * const hasWriteAccess = useHasWriteAccess();
 * // hasWriteAccess = true
 */
export const useHasWriteAccess = (): boolean => {
  const userQuery = useGetCurrentUser();
  return (
    userQuery.data?.roles.includes("Global administrator") ||
    userQuery.data?.roles.includes("Write") ||
    userQuery.data?.roles.includes("Manage") ||
    false
  );
};
