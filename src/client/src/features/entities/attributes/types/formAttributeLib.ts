import { AttributeLibAm, AttributeLibCm, UnitLibCm } from "@mimirorg/typelibrary-types";
import { FormUnitHelper, createEmptyFormUnitHelper } from "features/entities/units/types/FormUnitHelper";

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

export const toFormAttributeLib = (attribute: AttributeLibCm): FormAttributeLib => ({
  name: attribute.name,
  typeReference: attribute.typeReference,
  description: attribute.description,
  units: attribute.attributeUnits.map((x) => toFormUnitHelper(x.unit)),
  defaultUnit: toFormUnitHelper(attribute.attributeUnits.find((x) => x.isDefault === true)?.unit),
});

export const toFormUnitHelper = (unit: UnitLibCm | undefined): FormUnitHelper => {
  if (!unit) return createEmptyFormUnitHelper();
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
