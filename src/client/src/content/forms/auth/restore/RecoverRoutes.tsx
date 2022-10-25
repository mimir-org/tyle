import { RouteObject } from "react-router-dom";
import { Recover } from "./Recover";

export const RecoverPath = "/recover";

export const recoverRoutes: RouteObject[] = [{ path: RecoverPath, element: <Recover /> }];
