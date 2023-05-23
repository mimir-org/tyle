import { AttributeLibAm, AttributeLibCm, AttributeUnitLibCm } from "@mimirorg/typelibrary-types";
import { FormUnitHelper, createEmptyFormUnitHelper } from "features/entities/units/types/FormUnitHelper";

export interface FormAttributeLib extends Omit<AttributeLibAm, "attributeUnits"> {
  units: FormUnitHelper[];
  defaultUnit: FormUnitHelper | undefined;
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
  units: attribute.attributeUnits.map((x) => toFormUnitHelper(x)),
  defaultUnit: toFormUnitHelper(attribute.attributeUnits.find((x) => x.isDefault === true))
});

export const toFormUnitHelper = (attributeUnit: AttributeUnitLibCm | undefined): FormUnitHelper => {
  if (!attributeUnit) return createEmptyFormUnitHelper();
  return ({
    name: attributeUnit.unit.name,
    description: attributeUnit.unit.description,
    symbol: attributeUnit.unit.symbol,
    unitId: attributeUnit.unit.id
  });
}

export const createEmptyAttribute = (): FormAttributeLib => ({
  name: "",
  typeReference: "",
  description: "",
  units: [],
  defaultUnit: undefined
});
