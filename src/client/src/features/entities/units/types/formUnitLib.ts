import { UnitLibAm } from "@mimirorg/typelibrary-types";

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
