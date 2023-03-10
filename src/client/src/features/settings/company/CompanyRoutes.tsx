import { CreateCompany } from "features/settings/company/CreateCompany";
import { RouteObject } from "react-router-dom";

export const companyBasePath = "company";

export const companyRoutes: RouteObject[] = [{ path: companyBasePath, element: <CreateCompany /> }];
