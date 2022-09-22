import { RouteObject } from "react-router-dom";
import { AttributeForm } from "../../../../forms/attribute/AttributeForm";

export const attributeFormRoutes: RouteObject[] = [
  { path: "form/attribute", element: <AttributeForm /> },
  { path: "form/attribute/clone/:id", element: <AttributeForm /> },
];
