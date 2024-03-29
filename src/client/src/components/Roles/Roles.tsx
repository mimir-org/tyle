import Flexbox from "components/Flexbox";
import RadioFilters from "components/RadioFilters";
import SettingsSection from "components/SettingsSection";
import { useGetAllRolesMapped } from "hooks/useGetAllRolesMapped";
import { useGetAllUsersMapped } from "hooks/useGetAllUsersMapped";
import { useState } from "react";
import { useTheme } from "styled-components";
import { useUpdateUserRole } from "../../api/authorize.queries";
import { useGetCurrentUser } from "../../api/user.queries";
import { useSubmissionToast } from "../../helpers/form.helpers";
import { mapUserViewToUserItem } from "../../helpers/mappers.helpers";
import { UserItem } from "../../types/userItem";
import RoleDialog from "./RoleDialog";
import { roleFilters } from "./Roles.helpers";
import UserList from "./UserList";
import UserListItem from "./UserListItem";

const Roles = () => {
  const theme = useTheme();
  const [selectedRoleFilter, setSelectedRoleFilter] = useState(roleFilters[0]?.label);
  const userQuery = useGetCurrentUser();
  const currentUser = userQuery?.data != null ? mapUserViewToUserItem(userQuery.data) : undefined;
  const users = useGetAllUsersMapped();
  const roles = useGetAllRolesMapped();
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
    toast(updateUserRoleMutation.mutateAsync({ userId: user.id, roleId: newRoleId }));
  };

  return (
    <SettingsSection title="Roles">
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xxl}>
        <RadioFilters
          title=""
          filters={roleFilters}
          value={selectedRoleFilter}
          onChange={(x) => setSelectedRoleFilter(x)}
        />
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

export default Roles;
