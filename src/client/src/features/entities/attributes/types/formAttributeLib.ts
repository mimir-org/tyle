import { AttributeLibAm, AttributeLibCm, State, UnitLibCm } from "@mimirorg/typelibrary-types";
import { FormAttributeHelper } from "features/entities/types/formAttributeHelper";
import { FormUnitHelper } from "features/entities/units/types/FormUnitHelper";

export interface FormAttributeLib extends Omit<AttributeLibAm, "attributeUnits"> {
  units: FormUnitHelper[];
  state: State;
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
  const defaultUnit = attribute.attributeUnits.find((x) => x.isDefault)?.unit;

  return {
    name: attribute.name,
    typeReference: attribute.typeReference,
    description: attribute.description,
    state: attribute.state,
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
    state: unit.state,
  };
};

export const createEmptyAttribute = (): FormAttributeLib => ({
  name: "",
  typeReference: "",
  description: "",
  state: State.Draft,
  units: [],
  defaultUnit: null,
});

export const toFormAttributeHelper = (unit: AttributeLibCm): FormAttributeHelper => {
  return {
    name: unit.name,
    state: unit.state,
    description: unit.description,
    symbol: "",
    unitId: "",
  };
};
