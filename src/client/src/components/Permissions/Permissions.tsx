import { Flexbox } from "@mimirorg/component-library";
import RadioFilters from "components/RadioFilters";
import SettingsSection from "components/SettingsSection";
import { useState } from "react";
import { useTheme } from "styled-components";
import { getAllUsersMapped } from "./Permissions.helpers";
import UserList from "./UserList";
import UserListItem from "./UserListItem";
import PermissionDialog from "./PermissionDialog";
import { rolesOptions } from "./Permissions.helpers";
import { UserItem } from "../../types/userItem";

const Permissions = () => {
  const theme = useTheme();
  const [selectedRoleFilter, setSelectedRoleFilter] = useState(rolesOptions[0]?.label);
  const users = getAllUsersMapped();

  const filteredUsers = (): UserItem[] => {
    if(selectedRoleFilter === "All") return users;
    if(selectedRoleFilter === "None") return users.filter((user) => user.roles.length === 0);
    return users.filter((user) => user.roles.includes(selectedRoleFilter));
  };

  return (
    <SettingsSection title={"Roles"}>
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.xxl}>
        <RadioFilters
          title={""}
          filters={rolesOptions}
          value={selectedRoleFilter}
          onChange={(x) => setSelectedRoleFilter(x)}
        />
        <UserList title={"Users"}>
          {filteredUsers().map((user) => (
            <UserListItem
              key={user.id}
              name={user.name}
              role={user.roles}
              action={<PermissionDialog user={user} />}
            />
          ))}
        </UserList>
      </Flexbox>
    </SettingsSection>
  );
};

export default Permissions;
