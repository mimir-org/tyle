import { RouteObject } from "react-router-dom";
import Roles from "./Roles";

export const rolesBasePath = "permissions";

export const rolesRoutes: RouteObject[] = [{ path: rolesBasePath, element: <Roles /> }];
