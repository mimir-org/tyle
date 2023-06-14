import { Company } from "features/settings/company/Company";
import { RouteObject } from "react-router-dom";

export const createCompanyBasePath = "company/create";
export const updateCompanyBasePath = "company/update";

export const companyRoutes: RouteObject[] = [
  { path: createCompanyBasePath, element: <Company /> },
  { path: updateCompanyBasePath, element: <Company update={true} /> },
];
