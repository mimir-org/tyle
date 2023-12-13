import { Flexbox, toast } from "@mimirorg/component-library";
import RadioFilters from "components/RadioFilters";
import SettingsSection from "components/SettingsSection";
import { useState } from "react";
import { useTheme } from "styled-components";
import { getAllRolesMapped, getAllUsersMapped, toUserRoleRequest } from "./Permissions.helpers";
import UserList from "./UserList";
import UserListItem from "./UserListItem";
import PermissionDialog from "./PermissionDialog";
import { roleFilters } from "./Permissions.helpers";
import { UserItem } from "../../types/userItem";
import { useAddUserToRole, useRemoveUserFromRole } from "../../api/authorize.queries";
import { useSubmissionToast } from "../../helpers/form.helpers";

const Permissions = () => {
  const theme = useTheme();
  const [selectedRoleFilter, setSelectedRoleFilter] = useState(roleFilters[0]?.label);
  const users = getAllUsersMapped();
  const roles = getAllRolesMapped();
  const setRoleMutation = useAddUserToRole()
  const removeRoleMutation = useRemoveUserFromRole();

  const toast = useSubmissionToast("permission");

  const filteredUsers = (): UserItem[] => {
    if(selectedRoleFilter === "All") return users;
    if(selectedRoleFilter === "None") return users.filter((user) => user.roles.length === 0);
    return users.filter((user) => user.roles.includes(selectedRoleFilter));
  };

  const handleRoleChange = (user: UserItem, newRole: string | undefined) => {
    const newRoleId = roles.find((r) => r.roleName === newRole)?.roleId;
    const oldRoleId = roles.find((r) => r.roleId === user.roles[0])?.roleId;

    toast(removeRoleMutation.mutateAsync(toUserRoleRequest(user.id, oldRoleId)));
    toast(setRoleMutation.mutateAsync(toUserRoleRequest(user.id, newRoleId)));
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
