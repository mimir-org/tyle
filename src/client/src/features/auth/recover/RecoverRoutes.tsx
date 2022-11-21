import { Recover } from "features/auth/recover/Recover";
import { RouteObject } from "react-router-dom";

export const recoverBasePath = "/recover";

export const recoverRoutes: RouteObject[] = [{ path: recoverBasePath, element: <Recover /> }];
