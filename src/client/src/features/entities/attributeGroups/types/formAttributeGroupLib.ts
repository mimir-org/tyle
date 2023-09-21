import { AttributeGroupLibAm, AttributeGroupLibCm, UnitLibCm } from "@mimirorg/typelibrary-types";
import { FormUnitHelper } from "features/entities/units/types/FormUnitHelper";

export interface FormAttributeGroupLib extends Omit<AttributeGroupLibAm, "attributeGroupUnits"> {}

export const fromFormAttributeGroupLibToApiModel = (
  formAttributeGroup: FormAttributeGroupLib,
): AttributeGroupLibAm => ({
  name: formAttributeGroup.name,
  description: formAttributeGroup.description,
  attributeIds: formAttributeGroup.attributeIds,
});

export const toFormAttributeGroupLib = (attributeGroup: AttributeGroupLibCm): FormAttributeGroupLib => {
  return {
    name: attributeGroup.name,
    description: attributeGroup.description,
    attributeIds: attributeGroup.attributes.map((x) => x.id),
  };
};

export const toFormUnitHelper = (unit: UnitLibCm): FormUnitHelper => {
  return {
    name: unit.name,
    description: unit.description,
    symbol: unit.symbol,
    unitId: unit.id,
    state: unit.state,
  };
};

export const createEmptyAttributeGroup = (): FormAttributeGroupLib => ({
  name: "",
  description: "",
  attributeIds: [],
});
