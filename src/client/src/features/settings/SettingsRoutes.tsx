import { SettingsLayout } from "features/settings/layout/SettingsLayout";
import { RouteObject } from "react-router-dom";

export const SettingsPath = "/settings";

export const settingsRoutes: RouteObject = {
  path: SettingsPath,
  element: <SettingsLayout />,
  children: [],
};
