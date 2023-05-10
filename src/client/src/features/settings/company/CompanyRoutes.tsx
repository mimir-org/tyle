import { Company } from "features/settings/company/Company";
import { RouteObject } from "react-router-dom";

export const companyBasePath = "company";

export const companyRoutes: RouteObject[] = [{ path: companyBasePath, element: <Company /> }];
