import { RouteObject } from "react-router-dom";
import { TransportForm } from "../../../../forms/transport/TransportForm";

export const transportFormRoutes: RouteObject[] = [
  { path: "form/transport", element: <TransportForm /> },
  { path: "form/transport/clone/:id", element: <TransportForm /> },
  { path: "form/transport/edit/:id", element: <TransportForm isEdit /> },
];
