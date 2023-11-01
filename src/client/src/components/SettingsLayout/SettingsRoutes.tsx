import { accessRoutes } from "components/Access/AccessRoutes";
import { approvalRoutes } from "components/Approval/ApprovalRoutes";
import { companyRoutes } from "components/Company/CompanyRoutes";
import { permissionsRoutes } from "components/Permissions/PermissionsRoutes";
import { usersettingsBasePath, usersettingsRoutes } from "components/UserSettings/UserSettingsRoutes";
import { Navigate, RouteObject } from "react-router-dom";
import SettingsLayout from "./SettingsLayout";

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
