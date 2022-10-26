import { RouteObject } from "react-router-dom";
import { NodeForm } from "./NodeForm";

export const nodeFormBasePath = "form/node";

export const nodeFormRoutes: RouteObject[] = [
  { path: nodeFormBasePath, element: <NodeForm /> },
  { path: `${nodeFormBasePath}/clone/:id`, element: <NodeForm mode={"clone"} /> },
  { path: `${nodeFormBasePath}/edit/:id`, element: <NodeForm mode={"edit"} /> },
];
