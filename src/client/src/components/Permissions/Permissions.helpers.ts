import { Option } from "utils";
import { UserItem } from "../../types/userItem";
import { useGetAllUsers } from "../../api/user.queries";
import { mapRoleViewToRoleItem, mapUserViewToUserItem } from "../../helpers/mappers.helpers";
import { RoleItem } from "../../types/Role";
import { useGetRoles } from "../../api/authorize.queries";
import { UserRoleRequest } from "../../types/authentication/userRoleRequest";

export const roleFilters: Option<string>[] = [
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
};

export const getAllRolesMapped = (): RoleItem[] => {
  const roleQuery = useGetRoles();
  return roleQuery.data?.map((role) => mapRoleViewToRoleItem(role)) ?? []
};

export const toUserRoleRequest = (uId: string, rId: string | undefined): UserRoleRequest => ({
  userId: uId,
  roleId: rId ?? ""
});