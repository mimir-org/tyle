import { Option } from "utils";
import { UserItem } from "../../types/userItem";
import { useGetAllUsers } from "../../api/user.queries";
import { mapUserViewToUserItem } from "../../helpers/mappers.helpers";

export const rolesOptions: Option<string>[] = [
    {value: "-1", label: "All"},
    {value: "0", label: "None"},
    {value: "1", label: "Reader"},
    {value: "2", label: "Contributor"},
    {value: "3", label: "Reviewer"},
    {value: "4", label: "Administrator"}
];

export const getAllUsersMapped = (): UserItem[] => {
  const usersQuery = useGetAllUsers();
  return usersQuery.data?.map((user) => mapUserViewToUserItem(user)) ?? []
}