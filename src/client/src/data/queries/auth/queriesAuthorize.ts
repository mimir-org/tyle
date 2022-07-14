import { MimirorgPermissionAm, MimirorgUserRoleAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery } from "react-query";
import { apiAuthorize } from "../../api/auth/apiAuthorize";

const keys = {
  all: ["authorize"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetRoles = () => useQuery(keys.lists(), apiAuthorize.getRoles);

export const useAddUserToRole = () => useMutation((item: MimirorgUserRoleAm) => apiAuthorize.postAddUserRole(item));

export const useRemoveUserFromRole = () =>
  useMutation((item: MimirorgUserRoleAm) => apiAuthorize.postRemoveUserRole(item));

export const useGetPermissions = () => useQuery(keys.lists(), apiAuthorize.getPermissions);

export const useSetUserPermission = () =>
  useMutation((item: MimirorgPermissionAm) => apiAuthorize.postUserPermission(item));
