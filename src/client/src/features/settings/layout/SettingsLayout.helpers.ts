import { Link } from "common/types/link";
import { LinkGroup } from "common/types/linkGroup";
import { accessBasePath } from "features/settings/access/AccessRoutes";
import { permissionsBasePath } from "features/settings/permission/PermissionsRoutes";
import { approvalBasePath } from "features/settings/approval/ApprovalRoutes";
import { useTranslation } from "react-i18next";
import { usersettingsBasePath } from "../usersettings/UserSettingsRoutes";
import { createCompanyBasePath, updateCompanyBasePath } from "features/settings/company/CompanyRoutes";
import { useGetFilteredCompanies } from "hooks/filter-companies/useGetFilteredCompanies";
import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useGetRoles } from "hooks/useGetRoles";

export const useSettingsLinkGroups = (): LinkGroup[] => {
  const admLinks = useAdministerLinks();

  return [{ links: admLinks }];
};

const useAdministerLinks = (): Link[] => {
  const { t } = useTranslation("settings");

  const isGlobalAdmin = useGetRoles()?.includes("Global administrator");
  const managesCompanies = useGetFilteredCompanies(MimirorgPermission.Manage).length > 0;
  const hasApprovePermissionOrHigher = useGetFilteredCompanies(MimirorgPermission.Approve).length > 0;

  const result: Link[] = [
    {
      name: t("usersettings.title"),
      path: usersettingsBasePath,
    },
  ];

  if (hasApprovePermissionOrHigher) {
    result.push({
      name: t("approval.title"),
      path: approvalBasePath,
    });
  }

  if (managesCompanies) {
    result.push(
      {
        name: t("access.title"),
        path: accessBasePath,
      },
      {
        name: t("permissions.title"),
        path: permissionsBasePath,
      },
      {
        name: t("company.title.update"),
        path: updateCompanyBasePath,
      },
    );
  }

  if (isGlobalAdmin) {
    result.push({
      name: t("company.title.create"),
      path: createCompanyBasePath,
    });
  }

  return result;
};
