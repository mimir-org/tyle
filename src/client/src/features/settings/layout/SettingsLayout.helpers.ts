import { Link } from "common/types/link";
import { LinkGroup } from "common/types/linkGroup";
import { accessBasePath } from "features/settings/access/AccessRoutes";
import { permissionsBasePath } from "features/settings/permission/PermissionsRoutes";
import { approvalBasePath } from "features/settings/approval/ApprovalRoutes";
import { useTranslation } from "react-i18next";
import { usersettingsBasePath } from "../usersettings/UserSettingsRoutes";
import { companyBasePath } from "features/settings/company/CompanyRoutes";

export const useSettingsLinkGroups = (): LinkGroup[] => {
  const admLinks = useAdministerLinks();

  return [{ links: admLinks }];
};

const useAdministerLinks = (): Link[] => {
  const { t } = useTranslation("settings");

  return [
    {
      name: t("access.title"),
      path: accessBasePath,
    },
    {
      name: t("permissions.title"),
      path: permissionsBasePath,
    },
    {
      name: t("approval.title"),
      path: approvalBasePath,
    },
    {
      name: t("usersettings.title"),
      path: usersettingsBasePath,
    },
    {
      name: t("createCompany.title"),
      path: companyBasePath,
    },
  ];
};
