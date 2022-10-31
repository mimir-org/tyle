import { TransportForm } from "features/entities/transport/TransportForm";
import { RouteObject } from "react-router-dom";

export const transportFormBasePath = "form/transport";

export const transportFormRoutes: RouteObject[] = [
  { path: transportFormBasePath, element: <TransportForm /> },
  { path: `${transportFormBasePath}/clone/:id`, element: <TransportForm mode={"clone"} /> },
  { path: `${transportFormBasePath}/edit/:id`, element: <TransportForm mode={"edit"} /> },
];
