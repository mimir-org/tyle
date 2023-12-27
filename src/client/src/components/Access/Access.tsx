import { Flexbox } from "@mimirorg/component-library";
import SettingsSection from "components/SettingsSection";
import { useTheme } from "styled-components";
import { GetAllRolesMapped, GetAllUsersMapped, roleFilters, toUserRoleRequest } from "../Roles/Roles.helpers";
import { useState } from "react";
import UserList from "../Roles/UserList";
import UserListItem from "../Roles/UserListItem";
import RoleDialog from "../Roles/RoleDialog";
import { useGetCurrentUser } from "../../api/user.queries";
import { mapUserViewToUserItem } from "../../helpers/mappers.helpers";
import { useUpdateUserRole } from "../../api/authorize.queries";
import { UserItem } from "../../types/userItem";
import { useSubmissionToast } from "../../helpers/form.helpers";

const Access = () => {
  const theme = useTheme();

  const [selectedRoleFilter, ] = useState(roleFilters[0]?.label);
  const userQuery = useGetCurrentUser();
  const currentUser = userQuery?.data != null ? mapUserViewToUserItem(userQuery.data) : undefined;
  const users = GetAllUsersMapped().filter((e) => e.roles.length === 0);
  const roles = GetAllRolesMapped();
  const updateUserRoleMutation = useUpdateUserRole();
  const toast = useSubmissionToast("permission");
  const filteredUsers = (): UserItem[] => {
    const excludingCurrentUserList = users.filter((user) => user.id !== currentUser?.id);
    if (selectedRoleFilter === "All") return excludingCurrentUserList.filter((user) => user.id !== currentUser?.id);
    if (selectedRoleFilter === "None") return excludingCurrentUserList.filter((user) => user.roles.length === 0);
    return excludingCurrentUserList.filter((user) => user.roles.includes(selectedRoleFilter));
  };

  const handleRoleChange = (user: UserItem, newRole: string | undefined) => {
    const newRoleId = roles.find((r) => r.roleName === newRole)?.roleId ?? "";
    toast(updateUserRoleMutation.mutateAsync(toUserRoleRequest(user.id, newRoleId)));
  };
  return (
    <SettingsSection title="Roles">
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.xxl}>
        <UserList title="Users">
          {filteredUsers().map((user) => (
            <UserListItem
              key={user.id}
              name={user.name}
              role={user.roles}
              action={<RoleDialog user={user} handleRoleChange={handleRoleChange} />}
            />
          ))}
        </UserList>
      </Flexbox>
    </SettingsSection>
  );
};

export default Access;
