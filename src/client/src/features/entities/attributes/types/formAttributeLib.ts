import { AttributeLibAm, AttributeLibCm, State, UnitLibCm } from "@mimirorg/typelibrary-types";
import { AttributeTypeRequest } from "common/types/attributes/attributeTypeRequest";
import { AttributeView } from "common/types/attributes/attributeView";
import { ValueConstraintRequest } from "common/types/attributes/valueConstraintRequest";
import { ValueConstraintView } from "common/types/attributes/valueConstraintView";
import { FormAttributeHelper } from "features/entities/types/FormAttributeHelper";
import { FormUnitHelper } from "features/entities/units/types/FormUnitHelper";

export interface FormAttributeLib extends Omit<AttributeLibAm, "attributeUnits"> {
  units: FormUnitHelper[];
  state: State;
  defaultUnit: FormUnitHelper | null;
}

export const toAttributeTypeRequest = (attribute: AttributeView): AttributeTypeRequest => {
  return {
    name: attribute.name,
    description: attribute.description,
    predicateId: attribute.predicate?.id,
    unitIds: attribute.units.map(x => x.id),
    unitMinCount: attribute.unitMinCount,
    unitMaxCount: attribute.unitMaxCount,
    provenanceQualifier: attribute.provenanceQualifier,
    rangeQualifier: attribute.rangeQualifier,
    regularityQualifier: attribute.regularityQualifier,
    scopeQualifier: attribute.scopeQualifier,
    valueConstraint: toValueConstraintRequest(attribute.valueConstraint)
  };
};

export const toValueConstraintRequest = (valueConstraint: ValueConstraintView | undefined): ValueConstraintRequest | undefined => {
  if (valueConstraint === undefined) return undefined;

  return {
    constraintType: valueConstraint.constraintType,
    dataType: valueConstraint.dataType,
    minCount: valueConstraint.minCount,
    maxCount: valueConstraint.maxCount,
    value: valueConstraint.value ? valueConstraint.value.toString() : undefined,
    valueList: valueConstraint.valueList ? valueConstraint.valueList.map(x => x.toString()) : [],
    pattern: valueConstraint.pattern,
    minValue: valueConstraint.minValue,
    maxValue: valueConstraint.maxValue,
    minInclusive: valueConstraint.minInclusive,
    maxInclusive: valueConstraint.maxInclusive
  };
}

export const toFormUnitHelper = (unit: UnitLibCm): FormUnitHelper => {
  return {
    name: unit.name,
    description: unit.description,
    symbol: unit.symbol,
    unitId: unit.id,
    state: unit.state,
  };
};

export const createEmptyAttributeTypeRequest = (): AttributeTypeRequest => ({
  name: "",
  description: undefined,
  predicateId: undefined,
  unitIds: [],
  unitMinCount: 0,
  unitMaxCount: 0,
  provenanceQualifier: undefined,
  rangeQualifier: undefined,
  regularityQualifier: undefined,
  scopeQualifier: undefined,
  valueConstraint: undefined
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
