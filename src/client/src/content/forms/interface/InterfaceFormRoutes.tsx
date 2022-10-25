import { RouteObject } from "react-router-dom";
import { InterfaceForm } from "./InterfaceForm";

export const interfaceFormRoutes: RouteObject[] = [
  { path: "form/interface", element: <InterfaceForm /> },
  { path: "form/interface/clone/:id", element: <InterfaceForm mode={"clone"} /> },
  { path: "form/interface/edit/:id", element: <InterfaceForm mode={"edit"} /> },
];
