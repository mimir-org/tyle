import { RouteObject } from "react-router-dom";
import { DatumForm } from "./DatumForm";

export const datumFormBasePath = "form/datum";

export const datumFormRoutes: RouteObject[] = [
  { path: datumFormBasePath, element: <DatumForm /> },
  { path: `${datumFormBasePath}/clone/:id`, element: <DatumForm /> },
  { path: `${datumFormBasePath}/edit/:id`, element: <DatumForm /> },
];
