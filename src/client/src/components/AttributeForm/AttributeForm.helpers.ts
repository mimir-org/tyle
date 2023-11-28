import { useCreateAttribute, useUpdateAttribute } from "api/attribute.queries";
import { AttributeTypeRequest } from "types/attributes/attributeTypeRequest";
import { AttributeView } from "types/attributes/attributeView";
import { ConstraintType } from "types/attributes/constraintType";
import { ProvenanceQualifier } from "types/attributes/provenanceQualifier";
import { RangeQualifier } from "types/attributes/rangeQualifier";
import { RdlPredicate } from "types/attributes/rdlPredicate";
import { RdlUnit } from "types/attributes/rdlUnit";
import { RegularityQualifier } from "types/attributes/regularityQualifier";
import { ScopeQualifier } from "types/attributes/scopeQualifier";
import { XsdDataType } from "types/attributes/xsdDataType";
import { FormMode } from "types/formMode";
import { InfoItem } from "types/infoItem";

export const useAttributeMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateAttribute();
  const updateMutation = useUpdateAttribute(id ?? "");
  return mode === "edit" ? updateMutation : createMutation;
};

export interface AttributeFormFields {
  name: string;
  predicate: RdlPredicate | null;
  description: string;
  provenanceQualifier: ProvenanceQualifier | null;
  rangeQualifier: RangeQualifier | null;
  regularityQualifier: RegularityQualifier | null;
  scopeQualifier: ScopeQualifier | null;
  unitRequirement: UnitRequirement;
  units: RdlUnit[];
  valueConstraint: ValueConstraintFields;
}

export enum UnitRequirement {
  NoUnit = 0,
  Optional = 1,
  Required = 2,
}

export interface ValueConstraintFields {
  enabled: boolean;
  requireValue: boolean;
  constraintType: ConstraintType;
  dataType: XsdDataType;
  value: string;
  valueList: { id: string; value: string }[];
  pattern: string;
  minValue: string;
  maxValue: string;
}

export const toAttributeFormFields = (attributeView: AttributeView): AttributeFormFields => ({
  ...attributeView,
  description: attributeView.description ?? "",
  unitRequirement:
    attributeView.unitMinCount === 1
      ? UnitRequirement.Required
      : attributeView.unitMaxCount === 1
        ? UnitRequirement.Optional
        : UnitRequirement.NoUnit,
  valueConstraint: attributeView.valueConstraint
    ? {
        enabled: true,
        requireValue: attributeView.valueConstraint.minCount > 0,
        constraintType: attributeView.valueConstraint.constraintType,
        dataType: attributeView.valueConstraint.dataType,
        value: attributeView.valueConstraint.value?.toString() ?? "",
        valueList:
          attributeView.valueConstraint.valueList?.map((item) => ({
            id: crypto.randomUUID(),
            value: item.toString(),
          })) ?? [],
        pattern: attributeView.valueConstraint.pattern ?? "",
        minValue: attributeView.valueConstraint.minValue?.toString() ?? "",
        maxValue: attributeView.valueConstraint.maxValue?.toString() ?? "",
      }
    : createEmptyValueConstraintFields(),
});

export const toAttributeTypeRequest = (attributeFormFields: AttributeFormFields): AttributeTypeRequest => ({
  ...attributeFormFields,
  description: attributeFormFields.description ? attributeFormFields.description : null,
  predicateId: attributeFormFields.predicate?.id ?? null,
  unitIds: attributeFormFields.units.map((unit) => unit.id),
  unitMinCount: attributeFormFields.unitRequirement === UnitRequirement.Required ? 1 : 0,
  unitMaxCount: attributeFormFields.unitRequirement === UnitRequirement.NoUnit ? 0 : 1,
  valueConstraint: attributeFormFields.valueConstraint.enabled
    ? {
        constraintType: attributeFormFields.valueConstraint.constraintType,
        dataType: attributeFormFields.valueConstraint.dataType,
        value:
          attributeFormFields.valueConstraint.constraintType === ConstraintType.HasSpecificValue
            ? attributeFormFields.valueConstraint.value
            : null,
        valueList:
          attributeFormFields.valueConstraint.constraintType === ConstraintType.IsInListOfAllowedValues
            ? attributeFormFields.valueConstraint.valueList.map((item) => item.value)
            : [],
        pattern:
          attributeFormFields.valueConstraint.constraintType === ConstraintType.MatchesRegexPattern
            ? attributeFormFields.valueConstraint.pattern
            : null,
        minValue:
          attributeFormFields.valueConstraint.constraintType === ConstraintType.IsInNumberRange &&
          attributeFormFields.valueConstraint.minValue
            ? Number(attributeFormFields.valueConstraint.minValue)
            : null,
        maxValue:
          attributeFormFields.valueConstraint.constraintType === ConstraintType.IsInNumberRange &&
          attributeFormFields.valueConstraint.maxValue
            ? Number(attributeFormFields.valueConstraint.maxValue)
            : null,
        minCount: attributeFormFields.valueConstraint.requireValue ? 1 : 0,
        maxCount: null,
      }
    : null,
});

export const createEmptyAttributeFormFields = (): AttributeFormFields => ({
  name: "",
  predicate: null,
  description: "",
  provenanceQualifier: null,
  rangeQualifier: null,
  regularityQualifier: null,
  scopeQualifier: null,
  unitRequirement: UnitRequirement.NoUnit,
  units: [],
  valueConstraint: createEmptyValueConstraintFields(),
});

const createEmptyValueConstraintFields = (): ValueConstraintFields => ({
  enabled: false,
  requireValue: false,
  constraintType: ConstraintType.HasSpecificValue,
  dataType: XsdDataType.String,
  value: "",
  valueList: [],
  pattern: "",
  minValue: "",
  maxValue: "",
});

export const predicateInfoItem = (predicate: RdlPredicate): InfoItem => ({
  id: predicate.id.toString(),
  name: predicate.name,
  descriptors: {
    Description: predicate.description,
    IRI: predicate.iri,
  },
});

export const unitInfoItem = (unit: RdlUnit): InfoItem => ({
  id: unit.id.toString(),
  name: unit.symbol ? `${unit.name} (${unit.symbol})` : unit.name,
  descriptors: {
    Description: unit.description,
    IRI: unit.iri,
  },
});
