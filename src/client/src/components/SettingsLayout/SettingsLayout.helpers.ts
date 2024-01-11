import { useGetCurrentUser } from "api/user.queries";
import { accessBasePath } from "components/Access/AccessRoutes";
import { approvalBasePath } from "components/Approval/ApprovalRoutes";
import { rolesBasePath } from "components/Roles/RolesRoutes";
import { usersettingsBasePath } from "components/UserSettings/UserSettingsRoutes";
import { Link } from "types/link";
import { LinkGroup } from "types/linkGroup";

export const useSettingsLinkGroups = (): LinkGroup[] => {
  const admLinks = useAdministerLinks();

  return [{ links: admLinks }];
};

const useAdministerLinks = (): Link[] => {
  const currentUser = useGetCurrentUser();

  const links = [
    {
      name: "User settings",
      path: usersettingsBasePath,
    },
  ];

  if (currentUser.data?.roles.includes("Reviewer") || currentUser.data?.roles.includes("Administrator")) {
    links.push({
      name: "Approval",
      path: approvalBasePath,
    });
  }

  if (currentUser.data?.roles.includes("Administrator")) {
    links.push(
      {
        name: "Access",
        path: accessBasePath,
      },
      {
        name: "Roles",
        path: rolesBasePath,
      },
    );
  }

  return links;
};
