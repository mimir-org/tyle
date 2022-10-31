import { NodeForm } from "features/entities/node/NodeForm";
import { RouteObject } from "react-router-dom";

export const nodeFormBasePath = "form/node";

export const nodeFormRoutes: RouteObject[] = [
  { path: nodeFormBasePath, element: <NodeForm /> },
  { path: `${nodeFormBasePath}/clone/:id`, element: <NodeForm mode={"clone"} /> },
  { path: `${nodeFormBasePath}/edit/:id`, element: <NodeForm mode={"edit"} /> },
];
