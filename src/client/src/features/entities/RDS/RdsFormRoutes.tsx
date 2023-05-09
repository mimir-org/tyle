import { RouteObject } from "react-router-dom";
import { RdsForm } from "./RdsForm";

export const rdsFormBasePath = "form/rds";

export const rdsFormRoutes: RouteObject[] = [
  { path: rdsFormBasePath, element: <RdsForm /> },
  { path: `${rdsFormBasePath}/clone/:id`, element: <RdsForm /> },
  { path: `${rdsFormBasePath}/edit/:id`, element: <RdsForm /> },
];