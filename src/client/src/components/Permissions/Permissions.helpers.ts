import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { Option, getOptionsFromEnum } from "utils";
import { UserItem } from "../../types/userItem";
import { useGetAllUsers } from "../../api/user.queries";
import { mapUserViewToUserItem } from "../../helpers/mappers.helpers";

/**
 * Returns the permission options for the permission dropdown.
 * The options are based on the MimirorgPermission enum.
 * @returns {Option<string>[]} The permission options
 * @example
 * const permissionOptions = getPermissionOptions();
 * // permissionOptions = [
 * //   { value: "-1", label: "All" },
 * //   { value: "0", label: "None" },
 * //   { value: "1", label: "Read" },
 * //   { value: "2", label: "Write" },
 * //   { value: "3", label: "Manage" },
 * // ];
 */
export const getPermissionOptions = (): Option<string>[] => {
  const regularPermissions = getOptionsFromEnum<MimirorgPermission>(MimirorgPermission);
  const regularPermissionsMapped = regularPermissions.map((x) => ({
    value: String(x.value),
    label: x.label,
  }));

  return [{ value: "-1", label: "All" }, ...regularPermissionsMapped];
};

/*export const useFilteredUsers = (companyId: string, permission: UserItemPermission): UserItem[] => {
  const userQuery = useGetAuthCompanyUsers(companyId);
  const users = userQuery.data?.map((x) => mapMimirorgUserCmToUserItem(x)) ?? [];

  if (permission === MimirorgPermissionExtended.All) {
    return users.filter((user) => user.permissions[companyId]);
  }

  return users.filter((user) => user.permissions[companyId]?.value === permission);
};*/

export const getAllUsersMapped = (): UserItem[] => {
  const usersQuery = useGetAllUsers();
  return usersQuery.data?.map((user) => mapUserViewToUserItem(user)) ?? []
}