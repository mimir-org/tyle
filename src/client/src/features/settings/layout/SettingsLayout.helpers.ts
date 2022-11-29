import { Link } from "common/types/link";
import { LinkGroup } from "common/types/linkGroup";
import { accessBasePath } from "features/settings/access/AccessRoutes";
import { permissionsBasePath } from "features/settings/permission/PermissionsRoutes";
import { useTranslation } from "react-i18next";

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
  ];
};
