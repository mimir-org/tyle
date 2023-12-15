import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { UserRoleRequest } from "types/authentication/userRoleRequest";
import { authorizeApi } from "./authorize.api";
import { userKeys } from "./user.queries";

const keys = {
  all: ["authorize"] as const,
  lists: () => [...keys.all, "list"] as const,
  list: (filters: string) => [...keys.lists(), {filters}] as const,
};

export const useGetRoles = () => useQuery(keys.list(""), authorizeApi.getRoles);

export const useAddUserToRole = () => useMutation((item: UserRoleRequest) => authorizeApi.postAddUserRole(item));

export const useRemoveUserFromRole = () =>
  useMutation((item: UserRoleRequest) => authorizeApi.postRemoveUserRole(item));

export const useUpdateUserRole = () => {
  const queryClient = useQueryClient();

  return useMutation((item: UserRoleRequest) => authorizeApi.putUpdateUserRole(item),
    {
      onSuccess: () => {
        queryClient.invalidateQueries(userKeys.list(""));
      }
    })

}
