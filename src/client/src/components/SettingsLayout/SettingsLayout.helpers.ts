import { accessBasePath } from "components/Access/AccessRoutes";
import { approvalBasePath } from "components/Approval/ApprovalRoutes";
import { permissionsBasePath } from "components/Permissions/PermissionsRoutes";
import { usersettingsBasePath } from "components/UserSettings/UserSettingsRoutes";
import { useTranslation } from "react-i18next";
import { Link } from "types/link";
import { LinkGroup } from "types/linkGroup";

export const useSettingsLinkGroups = (): LinkGroup[] => {
  const admLinks = useAdministerLinks();

  return [{ links: admLinks }];
};

const useAdministerLinks = (): Link[] => {
  const { t } = useTranslation("settings");

  const result: Link[] = [
    {
      name: t("usersettings.title"),
      path: usersettingsBasePath,
    },
    {
      name: t("approval.title"),
      path: approvalBasePath,
    },
    {
      name: t("access.title"),
      path: accessBasePath,
    },
    {
      name: t("permissions.title"),
      path: permissionsBasePath,
    },
  ];

  return result;
};
