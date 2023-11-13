import AlternativeBlockForm from "components/AlternativeBlockForm";
import { RouteObject } from "react-router-dom";
import BlockForm from "./BlockForm";

export const blockFormBasePath = "form/block";

export const blockFormRoutes: RouteObject[] = [
  { path: blockFormBasePath, element: <AlternativeBlockForm /> },
  { path: `${blockFormBasePath}/clone/:id`, element: <BlockForm mode={"clone"} /> },
  { path: `${blockFormBasePath}/edit/:id`, element: <BlockForm mode={"edit"} /> },
];
