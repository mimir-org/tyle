import { AspectObjectForm } from "features/entities/aspectobject/AspectObjectForm";
import { RouteObject } from "react-router-dom";

export const aspectObjectFormBasePath = "form/aspectObject";

export const aspectObjectFormRoutes: RouteObject[] = [
  { path: aspectObjectFormBasePath, element: <AspectObjectForm /> },
  { path: `${aspectObjectFormBasePath}/clone/:id`, element: <AspectObjectForm mode={"clone"} /> },
  { path: `${aspectObjectFormBasePath}/edit/:id`, element: <AspectObjectForm mode={"edit"} /> },
];
