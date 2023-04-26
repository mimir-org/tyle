import { UnitLibAm } from "@mimirorg/typelibrary-types/index";

export const toUnitLibAm = (unit: UnitLibAm): UnitLibAm => ({
  ...unit,
  name: unit.name,
  typeReference: unit.typeReference,
  companyId: unit.companyId,
  symbol: unit.symbol,
  description: unit.description,
});

export const createEmptyUnit = (): UnitLibAm => ({
  name: "",
  typeReference: "",
  companyId: 1,
  symbol: "",
  description: "",
});
