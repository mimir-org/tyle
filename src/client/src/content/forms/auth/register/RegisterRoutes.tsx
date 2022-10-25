import { RouteObject } from "react-router-dom";
import { Register } from "./Register";

export const RegisterPath = "/register";

export const registerRoutes: RouteObject[] = [{ path: RegisterPath, element: <Register /> }];
