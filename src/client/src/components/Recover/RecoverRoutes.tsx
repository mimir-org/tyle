import { RouteObject } from "react-router-dom";
import Recover from "./Recover";

export const recoverBasePath = "/recover";

export const recoverRoutes: RouteObject[] = [{ path: recoverBasePath, element: <Recover /> }];
