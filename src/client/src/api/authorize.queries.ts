import { MimirorgUserPermissionAm, MimirorgUserRoleAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { authorizeApi } from "./authorize.api";
import { companyKeys } from "./company.queries";

const keys = {
  all: ["authorize"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetRoles = () => useQuery(keys.lists(), authorizeApi.getRoles);

export const useAddUserToRole = () => useMutation((item: MimirorgUserRoleAm) => authorizeApi.postAddUserRole(item));

export const useRemoveUserFromRole = () =>
  useMutation((item: MimirorgUserRoleAm) => authorizeApi.postRemoveUserRole(item));

export const useGetPermissions = () => useQuery(keys.lists(), authorizeApi.getPermissions);

export const useAddUserPermission = () => {
  const queryClient = useQueryClient();

  return useMutation((item: MimirorgUserPermissionAm) => authorizeApi.postAddUserPermission(item), {
    onSuccess: () => queryClient.invalidateQueries(companyKeys.allCompanyUsersLists),
  });
};

export const useRemoveUserPermission = () => {
  const queryClient = useQueryClient();

  return useMutation((item: MimirorgUserPermissionAm) => authorizeApi.postRemoveUserPermission(item), {
    onSuccess: () => queryClient.invalidateQueries(companyKeys.allCompanyUsersLists),
  });
};
