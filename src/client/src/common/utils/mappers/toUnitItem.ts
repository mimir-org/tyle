import { UnitLibCm } from "@mimirorg/typelibrary-types";
import { UnitItem } from "../../types/unitItem";

export const toUnitItem = (unit: UnitLibCm): UnitItem => {
  return {
    unitId: unit.id,
    id: unit.id,
    name: unit.name,
    description: unit.description,
    iri: unit.iri,
    typeReference: unit.typeReference,
    created: unit.created,
    createdBy: unit.createdBy,
    kind: "UnitItem",
    state: unit.state,
    symbol: unit.symbol,
    isDefault: false,
  };
};
