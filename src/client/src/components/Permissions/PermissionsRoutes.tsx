import { RouteObject } from "react-router-dom";
import Permissions from "./Permissions";

export const permissionsBasePath = "permissions";

export const permissionsRoutes: RouteObject[] = [{ path: permissionsBasePath, element: <Permissions /> }];
