import { useGetAllUsers } from "api/user.queries";
import { mapUserViewToUserItem } from "helpers/mappers.helpers";
import { UserItem } from "types/userItem";

export const useGetAllUsersMapped = (): UserItem[] => {
  const usersQuery = useGetAllUsers();
  return usersQuery.data?.map((user) => mapUserViewToUserItem(user)) ?? [];
};
