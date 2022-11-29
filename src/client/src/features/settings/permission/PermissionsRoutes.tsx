import { Permissions } from "features/settings/permission/Permissions";
import { RouteObject } from "react-router-dom";

export const permissionsBasePath = "permissions";

export const permissionsRoutes: RouteObject[] = [{ path: permissionsBasePath, element: <Permissions /> }];
