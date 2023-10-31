import { Access } from "components/Access/Access";
import { RouteObject } from "react-router-dom";

export const accessBasePath = "access";

export const accessRoutes: RouteObject[] = [{ path: accessBasePath, element: <Access /> }];
