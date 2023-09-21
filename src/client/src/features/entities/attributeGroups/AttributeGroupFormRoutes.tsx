import { RouteObject } from "react-router-dom";
import { AttributeGroupForm } from "./AttributeGroupForm";

export const attributeGroupFormBasePath = "form/attributeGroup";

export const attributeGroupFormRoutes: RouteObject[] = [
  { path: attributeGroupFormBasePath, element: <AttributeGroupForm /> },
  { path: `${attributeGroupFormBasePath}/clone/:id`, element: <AttributeGroupForm mode={"clone"} /> },
  { path: `${attributeGroupFormBasePath}/edit/:id`, element: <AttributeGroupForm mode={"edit"} /> },
];
