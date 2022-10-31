import { Recover } from "features/auth/recover/Recover";
import { RouteObject } from "react-router-dom";

export const RecoverPath = "/recover";

export const recoverRoutes: RouteObject[] = [{ path: RecoverPath, element: <Recover /> }];
