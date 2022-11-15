import { Register } from "features/auth/register/Register";
import { RouteObject } from "react-router-dom";

export const registerBasePath = "/register";

export const registerRoutes: RouteObject[] = [{ path: registerBasePath, element: <Register /> }];
