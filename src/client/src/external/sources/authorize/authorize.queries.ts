import { MimirorgPermissionAm, MimirorgUserRoleAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery } from "@tanstack/react-query";
import { authorizeApi } from "external/sources/authorize/authorize.api";

const keys = {
  all: ["authorize"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetRoles = () => useQuery(keys.lists(), authorizeApi.getRoles);

export const useAddUserToRole = () => useMutation((item: MimirorgUserRoleAm) => authorizeApi.postAddUserRole(item));

export const useRemoveUserFromRole = () =>
  useMutation((item: MimirorgUserRoleAm) => authorizeApi.postRemoveUserRole(item));

export const useGetPermissions = () => useQuery(keys.lists(), authorizeApi.getPermissions);

export const useSetUserPermission = () =>
  useMutation((item: MimirorgPermissionAm) => authorizeApi.postUserPermission(item));
