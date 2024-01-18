import { RouteObject } from "react-router-dom";
import UserSettings from "./UserSettings";

export const usersettingsBasePath = "usersettings";

export const usersettingsRoutes: RouteObject[] = [{ path: usersettingsBasePath, element: <UserSettings /> }];
