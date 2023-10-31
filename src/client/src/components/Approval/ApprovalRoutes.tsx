import { Approval } from "components/Approval/Approval";
import { RouteObject } from "react-router-dom";

export const approvalBasePath = "approval";

export const approvalRoutes: RouteObject[] = [{ path: approvalBasePath, element: <Approval /> }];
