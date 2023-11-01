import { RouteObject } from "react-router-dom";
import Approval from "./Approval";

export const approvalBasePath = "approval";

export const approvalRoutes: RouteObject[] = [{ path: approvalBasePath, element: <Approval /> }];
