import { Link } from "common/types/link";
import { LinkGroup } from "common/types/linkGroup";
import { accessBasePath } from "features/settings/access/AccessRoutes";
import { useTranslation } from "react-i18next";

export const useSettingsLinkGroups = (): LinkGroup[] => {
  const admLinks = useAdministerLinks();

  return [{ links: admLinks }];
};

const useAdministerLinks = (): Link[] => {
  const { t } = useTranslation();

  return [
    {
      name: t("settings.access.title"),
      path: accessBasePath,
    },
  ];
};
