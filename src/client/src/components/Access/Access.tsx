import { Flexbox } from "@mimirorg/component-library";
import SettingsSection from "components/SettingsSection";
import { useGetAllRolesMapped } from "hooks/useGetAllRolesMapped";
import { useGetAllUsersMapped } from "hooks/useGetAllUsersMapped";
import { useTheme } from "styled-components";
import { useUpdateUserRole } from "../../api/authorize.queries";
import { useSubmissionToast } from "../../helpers/form.helpers";
import { UserItem } from "../../types/userItem";
import RoleDialog from "../Roles/RoleDialog";
import UserList from "../Roles/UserList";
import UserListItem from "../Roles/UserListItem";
import AccessPlaceholder from "./AccessPlaceholder";

const Access = () => {
  const theme = useTheme();

  const users = useGetAllUsersMapped().filter((e) => e.roles.length === 0);
  const roles = useGetAllRolesMapped();
  const updateUserRoleMutation = useUpdateUserRole();

  const toast = useSubmissionToast("permission");

  const handleRoleChange = (user: UserItem, newRole: string | undefined) => {
    const newRoleId = roles.find((r) => r.roleName === newRole)?.roleId ?? "";
    toast(updateUserRoleMutation.mutateAsync({ userId: user.id, roleId: newRoleId }));
  };

  const showPlaceholder = users.length === 0;

  return (
    <SettingsSection title="Grant access to new users">
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.xxl}>
        {showPlaceholder ? (
          <AccessPlaceholder text="There are no new users in need of role assignment" />
        ) : (
          <UserList title="Users">
            {users.map((user) => (
              <UserListItem
                key={user.id}
                name={user.name}
                role={user.roles}
                action={<RoleDialog user={user} handleRoleChange={handleRoleChange} />}
              />
            ))}
          </UserList>
        )}
      </Flexbox>
    </SettingsSection>
  );
};

export default Access;
