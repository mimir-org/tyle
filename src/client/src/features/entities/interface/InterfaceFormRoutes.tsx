import { InterfaceForm } from "features/entities/interface/InterfaceForm";
import { RouteObject } from "react-router-dom";

export const interfaceFormBasePath = "form/interface";

export const interfaceFormRoutes: RouteObject[] = [
  { path: interfaceFormBasePath, element: <InterfaceForm /> },
  { path: `${interfaceFormBasePath}/clone/:id`, element: <InterfaceForm mode={"clone"} /> },
  { path: `${interfaceFormBasePath}/edit/:id`, element: <InterfaceForm mode={"edit"} /> },
];
