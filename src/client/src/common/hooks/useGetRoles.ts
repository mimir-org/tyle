import { useGetCurrentUser } from "external/sources/user/user.queries";

/**
 * Returns roles for the current user
 *
 */
export const useGetRoles = () => {
  const userQuery = useGetCurrentUser();
  return userQuery.data?.roles;
};
