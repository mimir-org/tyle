import { RouteObject } from "react-router-dom";
import Access from "./Access";

export const accessBasePath = "access";

export const accessRoutes: RouteObject[] = [{ path: accessBasePath, element: <Access /> }];
