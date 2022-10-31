import { Register } from "features/auth/register/Register";
import { RouteObject } from "react-router-dom";

export const RegisterPath = "/register";

export const registerRoutes: RouteObject[] = [{ path: RegisterPath, element: <Register /> }];
