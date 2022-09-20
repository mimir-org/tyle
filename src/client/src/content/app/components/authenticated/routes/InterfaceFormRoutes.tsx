import { RouteObject } from "react-router-dom";
import { InterfaceForm } from "../../../../forms/interface/InterfaceForm";

export const interfaceFormRoutes: RouteObject[] = [
  { path: "form/interface", element: <InterfaceForm /> },
  { path: "form/interface/clone/:id", element: <InterfaceForm /> },
  { path: "form/interface/edit/:id", element: <InterfaceForm isEdit /> },
];
