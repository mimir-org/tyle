import { accessBasePath } from "components/Access/AccessRoutes";
import { approvalBasePath } from "components/Approval/ApprovalRoutes";
import { rolesBasePath } from "components/Permissions/RolesRoutes";
import { usersettingsBasePath } from "components/UserSettings/UserSettingsRoutes";
import { Link } from "types/link";
import { LinkGroup } from "types/linkGroup";

export const useSettingsLinkGroups = (): LinkGroup[] => {
  const admLinks = useAdministerLinks();

  return [{ links: admLinks }];
};

const useAdministerLinks = (): Link[] => {
  return [
    {
      name: "User settings",
      path: usersettingsBasePath,
    },
    {
      name: "Approval",
      path: approvalBasePath,
    },
    {
      name: "Access",
      path: accessBasePath,
    },
    {
      name: "Roles",
      path: rolesBasePath,
    },
  ];
};
