import { useGetRoles } from "api/authorize.queries";
import { mapRoleViewToRoleItem } from "helpers/mappers.helpers";
import { RoleItem } from "types/role";

export const useGetAllRolesMapped = (): RoleItem[] => {
  const roleQuery = useGetRoles();
  return roleQuery.data?.map((role) => mapRoleViewToRoleItem(role)) ?? [];
};
