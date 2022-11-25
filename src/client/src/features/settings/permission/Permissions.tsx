import { Flexbox } from "complib/layouts";
import { RadioFilters } from "features/settings/common/radio-filters/RadioFilters";
import { SettingsSection } from "features/settings/common/settings-section/SettingsSection";
import {
  getPermissionOptions,
  useCompanyOptions,
  useDefaultCompanyOptions,
} from "features/settings/permission/Permissions.helpers";
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
      </Flexbox>
    </SettingsSection>
  );
};
