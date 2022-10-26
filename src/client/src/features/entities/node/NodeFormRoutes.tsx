import { RouteObject } from "react-router-dom";
import { NodeForm } from "./NodeForm";

export const nodeFormRoutes: RouteObject[] = [
  { path: "form/node", element: <NodeForm /> },
  { path: "form/node/clone/:id", element: <NodeForm mode={"clone"} /> },
  { path: "form/node/edit/:id", element: <NodeForm mode={"edit"} /> },
];
