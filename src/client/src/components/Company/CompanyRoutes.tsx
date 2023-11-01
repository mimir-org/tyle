import { RouteObject } from "react-router-dom";
import Company from "./Company";

export const createCompanyBasePath = "company/create";
export const updateCompanyBasePath = "company/update";

export const companyRoutes: RouteObject[] = [
  { path: createCompanyBasePath, element: <Company /> },
  { path: updateCompanyBasePath, element: <Company update={true} /> },
];
