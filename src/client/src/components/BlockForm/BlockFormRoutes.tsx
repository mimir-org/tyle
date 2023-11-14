import { RouteObject } from "react-router-dom";
import AlternativeBlockForm from "./BlockForm";

export const blockFormBasePath = "form/block";

export const blockFormRoutes: RouteObject[] = [
  { path: blockFormBasePath, element: <AlternativeBlockForm /> },
  { path: `${blockFormBasePath}/clone/:id`, element: <AlternativeBlockForm mode={"clone"} /> },
  { path: `${blockFormBasePath}/edit/:id`, element: <AlternativeBlockForm mode={"edit"} /> },
];
