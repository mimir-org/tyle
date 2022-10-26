import { RouteObject } from "react-router-dom";
import { TransportForm } from "./TransportForm";

export const transportFormBasePath = "form/transport";

export const transportFormRoutes: RouteObject[] = [
  { path: transportFormBasePath, element: <TransportForm /> },
  { path: `${transportFormBasePath}/clone/:id`, element: <TransportForm mode={"clone"} /> },
  { path: `${transportFormBasePath}/edit/:id`, element: <TransportForm mode={"edit"} /> },
];
