import { RouteObject } from "react-router-dom";
import { AttributeForm } from "./AttributeForm";

export const attributeFormBasePath = "form/attribute";

export const attributeFormRoutes: RouteObject[] = [
  { path: attributeFormBasePath, element: <AttributeForm /> },
  { path: `${attributeFormBasePath}/clone/:id`, element: <AttributeForm /> },
  { path: `${attributeFormBasePath}/edit/:id`, element: <AttributeForm /> },
];
