import { AttributeLibAm, AttributeLibCm, UnitLibCm } from "@mimirorg/typelibrary-types";
import { FormUnitHelper } from "features/entities/units/types/FormUnitHelper";

export interface FormAttributeLib extends Omit<AttributeLibAm, "attributeUnits"> {
  units: FormUnitHelper[];
  defaultUnit: FormUnitHelper | null;
}

export const fromFormAttributeLibToApiModel = (formAttribute: FormAttributeLib): AttributeLibAm => ({
  name: formAttribute.name,
  typeReference: formAttribute.typeReference,
  description: formAttribute.description,
  attributeUnits: formAttribute.units.map((x: FormUnitHelper) => ({
    unitId: x.unitId,
    isDefault: x.unitId === formAttribute.defaultUnit?.unitId,
  })),
});

export const toFormAttributeLib = (attribute: AttributeLibCm): FormAttributeLib => {
  const defaultUnit = attribute.attributeUnits.find((x) => x.isDefault === true)?.unit;

  return {
    name: attribute.name,
    typeReference: attribute.typeReference,
    description: attribute.description,
    units: attribute.attributeUnits.map((x) => toFormUnitHelper(x.unit)),
    defaultUnit: defaultUnit ? toFormUnitHelper(defaultUnit) : null,
  };
};

export const toFormUnitHelper = (unit: UnitLibCm): FormUnitHelper => {
  return {
    name: unit.name,
    description: unit.description,
    symbol: unit.symbol,
    unitId: unit.id,
  };
};

export const createEmptyAttribute = (): FormAttributeLib => ({
  name: "",
  typeReference: "",
  description: "",
  units: [],
  defaultUnit: null,
});
