import { Flexbox } from "@mimirorg/component-library";
import RadioFilters from "components/RadioFilters";
import SettingsSection from "components/SettingsSection";
import { useState } from "react";
import { useTheme } from "styled-components";
import { GetAllRolesMapped, GetAllUsersMapped, toUserRoleRequest, roleFilters } from "./permissions.helpers";
import UserList from "./UserList";
import UserListItem from "./UserListItem";
import PermissionDialog from "./PermissionDialog";
import { UserItem } from "../../types/userItem";
import { useUpdateUserRole } from "../../api/authorize.queries";
import { useSubmissionToast } from "../../helpers/form.helpers";
import { useGetCurrentUser } from "../../api/user.queries";
import { mapUserViewToUserItem } from "../../helpers/mappers.helpers";

const Permissions = () => {
  const theme = useTheme();
  const [selectedRoleFilter, setSelectedRoleFilter] = useState(roleFilters[0]?.label);
  const userQuery = useGetCurrentUser();
  const currentUser = userQuery?.data != null ? mapUserViewToUserItem(userQuery.data) : undefined;
  const users = GetAllUsersMapped();
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
    <SettingsSection title={"Roles"}>
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.xxl}>
        <RadioFilters
          title={""}
          filters={roleFilters}
          value={selectedRoleFilter}
          onChange={(x) => setSelectedRoleFilter(x)}
        />
        <UserList title={"Users"}>
          {filteredUsers().map((user) => (
            <UserListItem
              key={user.id}
              name={user.name}
              role={user.roles}
              action={<PermissionDialog user={user} handleRoleChange={handleRoleChange} />}
            />
          ))}
        </UserList>
      </Flexbox>
    </SettingsSection>
  );
};

export default Permissions;
