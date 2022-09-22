import { RouteObject } from "react-router-dom";
import { TransportForm } from "../../../../forms/transport/TransportForm";

export const transportFormRoutes: RouteObject[] = [
  { path: "form/transport", element: <TransportForm /> },
  { path: "form/transport/clone/:id", element: <TransportForm mode={"clone"} /> },
  { path: "form/transport/edit/:id", element: <TransportForm mode={"edit"} /> },
];
