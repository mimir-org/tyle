import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { UserRoleRequest } from "types/authentication/userRoleRequest";
import { authorizeApi } from "./authorize.api";
import { userKeys } from "./user.queries";

const keys = {
  all: ["authorize"] as const,
  lists: () => [...keys.all, "list"] as const,
  list: (filters: string) => [...keys.lists(), { filters }] as const,
};

export const useGetRoles = () => useQuery({ queryKey: keys.list(""), queryFn: authorizeApi.getRoles });

export const useAddUserToRole = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: UserRoleRequest) => authorizeApi.postAddUserRole(item),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: userKeys.list("") });
    },
  });
};

export const useRemoveUserFromRole = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: UserRoleRequest) => authorizeApi.postRemoveUserRole(item),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: userKeys.list("") });
    },
  });
};

export const useUpdateUserRole = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: UserRoleRequest) => authorizeApi.putUpdateUserRole(item),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: userKeys.list("") });
    },
  });
};
