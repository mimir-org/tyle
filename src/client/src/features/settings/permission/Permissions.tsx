import { Flexbox } from "complib/layouts";
import { RadioFilters } from "features/settings/common/radio-filters/RadioFilters";
import { SettingsSection } from "features/settings/common/settings-section/SettingsSection";
import {
  getPermissionOptions,
  useCompanyOptions,
  useDefaultCompanyOptions,
  useFilteredUsers,
} from "features/settings/permission/Permissions.helpers";
import { UserItemPermission } from "features/settings/permission/types/userItemPermission";
import { UserList } from "features/settings/permission/user-list/UserList";
import { UserListItem } from "features/settings/permission/user-list/UserListItem";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const Permissions = () => {
  const theme = useTheme();
  const { t } = useTranslation();

  const companies = useCompanyOptions();
  const [selectedCompany, setSelectedCompany] = useState(companies[0]?.value);
  useDefaultCompanyOptions(companies, selectedCompany, setSelectedCompany);

  const permissions = getPermissionOptions();
  const [selectedPermission, setSelectedPermission] = useState(permissions[0]?.value);

  const users = useFilteredUsers(selectedCompany, selectedPermission as unknown as UserItemPermission);

  return (
    <SettingsSection title={t("settings.permissions.title")}>
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xxl}>
        <RadioFilters
          title={t("settings.permissions.organization")}
          filters={companies}
          value={selectedCompany}
          onChange={(x) => setSelectedCompany(String(x))}
        />
        <RadioFilters
          title={t("settings.permissions.permission")}
          filters={permissions}
          value={selectedPermission}
          onChange={(x) => setSelectedPermission(x)}
        />
        <UserList title={t("settings.permissions.users")}>
          {users.map((user) => (
            <UserListItem key={user.id} name={user.name} trait={user.permissions[user.company.id]?.label} />
          ))}
        </UserList>
      </Flexbox>
    </SettingsSection>
  );
};
