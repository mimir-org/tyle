import { RouteObject } from "react-router-dom";
import { QuantityDatumForm } from "./QuantityDatumForm";

export const datumFormBasePath = "form/quantityDatum";

export const quantityDatumFormRoutes: RouteObject[] = [
  { path: datumFormBasePath, element: <QuantityDatumForm /> },
  { path: `${datumFormBasePath}/clone/:id`, element: <QuantityDatumForm /> },
  { path: `${datumFormBasePath}/edit/:id`, element: <QuantityDatumForm /> },
];
