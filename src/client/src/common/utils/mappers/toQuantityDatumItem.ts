import { QuantityDatumLibCm } from "@mimirorg/typelibrary-types";
import { QuantityDatumItem } from "../../types/quantityDatumItem";

export const toQuantityDatumItem = (datum: QuantityDatumLibCm): QuantityDatumItem => {
  return {
    id: datum.id,
    name: datum.name,
    description: datum.description,
    quantityType: datum.quantityDatumType,
    typeReference: datum.typeReference,
    kind: "QuantityDatumItem",
    state: datum.state,
  };
};
