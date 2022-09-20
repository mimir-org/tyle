import { RouteObject } from "react-router-dom";
import { AttributeForm } from "../../../../forms/attribute/AttributeForm";
import { InterfaceForm } from "../../../../forms/interface/InterfaceForm";

export const attributeFormRoutes: RouteObject[] = [
  { path: "form/attribute", element: <AttributeForm /> },
  { path: "form/attribute/clone/:id", element: <InterfaceForm /> },
];
