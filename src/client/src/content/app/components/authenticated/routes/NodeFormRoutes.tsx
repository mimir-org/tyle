import { RouteObject } from "react-router-dom";
import { NodeForm } from "../../../../forms/node/NodeForm";

export const nodeFormRoutes: RouteObject[] = [
  { path: "form/node", element: <NodeForm /> },
  { path: "form/node/clone/:id", element: <NodeForm /> },
  { path: "form/node/edit/:id", element: <NodeForm isEdit /> },
];
