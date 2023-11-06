import { useMutation, useQuery } from "@tanstack/react-query";
import { UserRoleRequest } from "types/authentication/userRoleRequest";
import { authorizeApi } from "./authorize.api";

const keys = {
  all: ["authorize"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetRoles = () => useQuery(keys.lists(), authorizeApi.getRoles);

export const useAddUserToRole = () => useMutation((item: UserRoleRequest) => authorizeApi.postAddUserRole(item));

export const useRemoveUserFromRole = () =>
  useMutation((item: UserRoleRequest) => authorizeApi.postRemoveUserRole(item));
