import { RouteObject } from "react-router-dom";
import { UnitForm } from "./UnitForm";

export const unitFormBasePath = "form/unit";

export const unitFormRoutes: RouteObject[] = [
  { path: unitFormBasePath, element: <UnitForm /> },
  { path: `${unitFormBasePath}/clone/:id`, element: <UnitForm /> },
  { path: `${unitFormBasePath}/edit/:id`, element: <UnitForm /> },
];
