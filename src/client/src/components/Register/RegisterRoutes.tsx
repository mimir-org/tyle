import { RouteObject } from "react-router-dom";
import Register from "./Register";

export const registerBasePath = "/register";

export const registerRoutes: RouteObject[] = [{ path: registerBasePath, element: <Register /> }];
