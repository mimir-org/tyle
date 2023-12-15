import { accessRoutes } from "components/Access/AccessRoutes";
import { approvalRoutes } from "components/Approval/ApprovalRoutes";
import { rolesRoutes } from "components/Permissions/RolesRoutes";
import { usersettingsBasePath, usersettingsRoutes } from "components/UserSettings/UserSettingsRoutes";
import { Navigate, RouteObject } from "react-router-dom";
import SettingsLayout from "./SettingsLayout";

export const settingsBasePath = "/settings";

export const settingsRoutes: RouteObject = {
  path: settingsBasePath,
  element: <SettingsLayout />,
  children: [
    ...accessRoutes,
    ...rolesRoutes,
    ...approvalRoutes,
    ...usersettingsRoutes,
    { path: settingsBasePath, element: <Navigate to={`${settingsBasePath}/${usersettingsBasePath}`} replace /> },
  ],
};
