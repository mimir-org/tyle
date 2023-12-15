import { RouteObject } from "react-router-dom";
import Permissions from "./Permissions";

export const rolesBasePath = "permissions";

export const rolesRoutes: RouteObject[] = [{ path: rolesBasePath, element: <Permissions /> }];
