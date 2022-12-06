import { Approval } from "features/settings/approval/Approval";
import { RouteObject } from "react-router-dom";

export const approvalBasePath = "approval";

export const approvalRoutes: RouteObject[] = [{ path: approvalBasePath, element: <Approval /> }];
