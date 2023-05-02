import { UserSettings } from "features/settings/usersettings/UserSettings";
import { RouteObject } from "react-router-dom";

export const usersettingsBasePath = "usersettings";

export const usersettingsRoutes: RouteObject[] = [{ path: usersettingsBasePath, element: <UserSettings /> }];
