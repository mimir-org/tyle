import { accessRoutes } from "components/Access/AccessRoutes";
import { approvalRoutes } from "components/Approval/ApprovalRoutes";
import { SettingsLayout } from "components/Settings/SettingsLayout";
import { permissionsRoutes } from "components/Permissions/PermissionsRoutes";
import { Navigate, RouteObject } from "react-router-dom";
import { companyRoutes } from "components/Company/CompanyRoutes";
import { usersettingsBasePath, usersettingsRoutes } from "components/UserSettings/UserSettingsRoutes";

export const settingsBasePath = "/settings";

export const settingsRoutes: RouteObject = {
  path: settingsBasePath,
  element: <SettingsLayout />,
  children: [
    ...accessRoutes,
    ...permissionsRoutes,
    ...approvalRoutes,
    ...usersettingsRoutes,
    ...companyRoutes,
    { path: settingsBasePath, element: <Navigate to={`${settingsBasePath}/${usersettingsBasePath}`} replace /> },
  ],
};
