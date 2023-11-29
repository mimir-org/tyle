import { Flexbox } from "@mimirorg/component-library";
import RadioFilters from "components/RadioFilters";
import SettingsSection from "components/SettingsSection";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { getAllUsersMapped, getPermissionOptions } from "./Permissions.helpers";
import UserList from "./UserList";
import UserListItem from "./UserListItem";
import PermissionDialog from "./PermissionDialog";

const Permissions = () => {
  const theme = useTheme();
  const { t } = useTranslation("settings");

  const permissions = getPermissionOptions();
  const [selectedPermission, setSelectedPermission] = useState(permissions[0]?.value);

  const users = getAllUsersMapped();

  return (
    <SettingsSection title={t("permissions.title")}>
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.xxl}>
        <RadioFilters
          title={t("permissions.permission")}
          filters={permissions}
          value={selectedPermission}
          onChange={(x) => setSelectedPermission(x)}
        />
        <UserList title={t("permissions.users")}>
          {users.map((user) => (
            <UserListItem
              key={user.id}
              name={user.name}
              trait={user.purpose}
              action={<PermissionDialog user={user} />}
            />
          ))}
        </UserList>
      </Flexbox>
    </SettingsSection>
  );
};

export default Permissions;
