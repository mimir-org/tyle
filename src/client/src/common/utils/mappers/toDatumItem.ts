import { QuantityDatumLibCm } from "@mimirorg/typelibrary-types";
import { DatumItem } from "../../types/datumItem";

export const toDatumItem = (datum: QuantityDatumLibCm): DatumItem => {
  return {
    id: datum.id,
    name: datum.name,
    description: datum.description,
    quantityType: datum.quantityDatumType,
    typeReference: datum.typeReference,
    kind: "DatumItem",
    state: datum.state,
  };
};
