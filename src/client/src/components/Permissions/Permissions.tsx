import { Flexbox } from "@mimirorg/component-library";
import RadioFilters from "components/RadioFilters";
import SettingsSection from "components/SettingsSection";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import PermissionDialog from "./PermissionDialog";
import {
  getPermissionOptions,
  useCompanyOptions,
  useDefaultCompanyOptions,
  useFilteredUsers,
} from "./Permissions.helpers";
import UserList from "./UserList";
import UserListItem from "./UserListItem";
import { UserItemPermission } from "./userItemPermission";

const Permissions = () => {
  const theme = useTheme();
  const { t } = useTranslation("settings");

  const companies = useCompanyOptions();
  const [selectedCompany, setSelectedCompany] = useState(companies[0]?.value);
  useDefaultCompanyOptions(companies, selectedCompany, setSelectedCompany);

  const permissions = getPermissionOptions();
  const [selectedPermission, setSelectedPermission] = useState(permissions[0]?.value);

  const users = useFilteredUsers(selectedCompany, Number(selectedPermission) as UserItemPermission);

  return (
    <SettingsSection title={t("permissions.title")}>
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.xxl}>
        <RadioFilters
          title={t("permissions.organization")}
          filters={companies}
          value={selectedCompany}
          onChange={(x) => setSelectedCompany(String(x))}
        />
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
              trait={user.permissions[selectedCompany]?.label}
              action={<PermissionDialog user={user} />}
            />
          ))}
        </UserList>
      </Flexbox>
    </SettingsSection>
  );
};

export default Permissions;
