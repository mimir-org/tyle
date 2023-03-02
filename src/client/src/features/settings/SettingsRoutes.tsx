import { accessBasePath, accessRoutes } from "features/settings/access/AccessRoutes";
import { approvalRoutes } from "features/settings/approval/ApprovalRoutes";
import { SettingsLayout } from "features/settings/layout/SettingsLayout";
import { permissionsRoutes } from "features/settings/permission/PermissionsRoutes";
import { Navigate, RouteObject } from "react-router-dom";
import { usersettingsRoutes } from "./usersettings/UserSettingsRoutes";

export const settingsBasePath = "/settings";

export const settingsRoutes: RouteObject = {
  path: settingsBasePath,
  element: <SettingsLayout />,
  children: [
    ...accessRoutes,
    ...permissionsRoutes,
    ...approvalRoutes,
    ...usersettingsRoutes,
    { path: settingsBasePath, element: <Navigate to={`${settingsBasePath}/${accessBasePath}`} replace /> },
  ],
};
