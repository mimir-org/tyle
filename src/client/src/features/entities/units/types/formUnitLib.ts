import { UnitLibAm } from "@mimirorg/typelibrary-types";

/**
 * Convert a unit from api model to form model.
 * @interface UnitLibAm
 * @param unit
 */
export const toUnitLibAm = (unit: UnitLibAm): UnitLibAm => ({
  ...unit,
  name: unit.name,
  typeReference: unit.typeReference,
  symbol: unit.symbol,
  description: unit.description,
});

export const createEmptyUnit = (): UnitLibAm => ({
  name: "",
  typeReference: "",
  symbol: "",
  description: "",
});
