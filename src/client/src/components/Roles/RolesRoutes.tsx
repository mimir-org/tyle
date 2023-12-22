import { RouteObject } from "react-router-dom";
import Roles from "./Roles";

export const rolesBasePath = "roles";

export const rolesRoutes: RouteObject[] = [{ path: rolesBasePath, element: <Roles /> }];
