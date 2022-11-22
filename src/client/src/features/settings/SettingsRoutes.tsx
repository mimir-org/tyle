import { accessBasePath, accessRoutes } from "features/settings/access/AccessRoutes";
import { SettingsLayout } from "features/settings/layout/SettingsLayout";
import { Navigate, RouteObject } from "react-router-dom";

export const settingsBasePath = "/settings";

export const settingsRoutes: RouteObject = {
  path: settingsBasePath,
  element: <SettingsLayout />,
  children: [
    ...accessRoutes,
    { path: settingsBasePath, element: <Navigate to={`${settingsBasePath}/${accessBasePath}`} replace /> },
  ],
};
