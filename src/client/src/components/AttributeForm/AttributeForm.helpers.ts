import { useCreateAttribute, useGetAttribute, useUpdateAttribute } from "api/attribute.queries";
import { useParams } from "react-router-dom";
import { AttributeTypeRequest } from "types/attributes/attributeTypeRequest";
import { AttributeView } from "types/attributes/attributeView";
import { ProvenanceQualifier } from "types/attributes/provenanceQualifier";
import { RangeQualifier } from "types/attributes/rangeQualifier";
import { RdlPredicate } from "types/attributes/rdlPredicate";
import { RdlUnit } from "types/attributes/rdlUnit";
import { RegularityQualifier } from "types/attributes/regularityQualifier";
import { ScopeQualifier } from "types/attributes/scopeQualifier";
import { FormMode } from "types/formMode";
import { InfoItem } from "types/infoItem";
import { ValueConstraintFields, getNotSetValueConstraintFields } from "./ValueConstraintStep.helpers";

export const useAttributeQuery = () => {
  const { id } = useParams();
  return useGetAttribute(id);
};

export const useAttributeMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateAttribute();
  const updateMutation = useUpdateAttribute(id ?? "");
  return mode === "edit" ? updateMutation : createMutation;
};

export interface AttributeFormFields {
  base: AttributeBaseFields;
  qualifiers: AttributeQualifierFields;
  unitRequirement: UnitRequirement;
  units: RdlUnit[];
  valueConstraint: ValueConstraintFields;
}

export interface AttributeBaseFields {
  name: string;
  predicate: RdlPredicate | null;
  description: string;
}

export interface AttributeQualifierFields {
  provenance: ProvenanceQualifier | null;
  range: RangeQualifier | null;
  regularity: RegularityQualifier | null;
  scope: ScopeQualifier | null;
}

export enum UnitRequirement {
  NoUnit = 0,
  Optional = 1,
  Required = 2,
}

export const toAttributeFormFields = (attributeView: AttributeView): AttributeFormFields => ({
  base: {
    name: attributeView.name,
    predicate: attributeView.predicate,
    description: attributeView.description ?? "",
  },
  qualifiers: {
    provenance: attributeView.provenanceQualifier,
    range: attributeView.rangeQualifier,
    regularity: attributeView.regularityQualifier,
    scope: attributeView.scopeQualifier,
  },
  unitRequirement:
    attributeView.unitMinCount === 1
      ? UnitRequirement.Required
      : attributeView.unitMaxCount === 1
        ? UnitRequirement.Optional
        : UnitRequirement.NoUnit,
  units: attributeView.units,
  valueConstraint: attributeView.valueConstraint
    ? {
        set: true,
        constraintType: attributeView.valueConstraint.constraintType,
        dataType: attributeView.valueConstraint.dataType,
        value: attributeView.valueConstraint.value?.toString() ?? "",
        valueList:
          attributeView.valueConstraint.valueList?.map((value) => ({
            id: crypto.randomUUID(),
            value: value.toString(),
          })) ?? [],
        pattern: attributeView.valueConstraint.pattern?.toString() ?? "",
        minValue: attributeView.valueConstraint.minValue?.toString() ?? "",
        maxValue: attributeView.valueConstraint.maxValue?.toString() ?? "",
        requireValue: attributeView.valueConstraint.minCount > 0,
      }
    : getNotSetValueConstraintFields(),
});

export const toAttributeTypeRequest = (attributeFormFields: AttributeFormFields): AttributeTypeRequest => ({
  name: attributeFormFields.base.name,
  description: attributeFormFields.base.description ? attributeFormFields.base.description : null,
  predicateId: attributeFormFields.base.predicate?.id ?? null,
  unitIds: attributeFormFields.units.map((unit) => unit.id),
  unitMinCount: attributeFormFields.unitRequirement === UnitRequirement.Required ? 1 : 0,
  unitMaxCount: attributeFormFields.unitRequirement === UnitRequirement.NoUnit ? 0 : 1,
  provenanceQualifier: attributeFormFields.qualifiers.provenance,
  rangeQualifier: attributeFormFields.qualifiers.range,
  regularityQualifier: attributeFormFields.qualifiers.regularity,
  scopeQualifier: attributeFormFields.qualifiers.scope,
  valueConstraint: attributeFormFields.valueConstraint.set
    ? {
        constraintType: attributeFormFields.valueConstraint.constraintType,
        dataType: attributeFormFields.valueConstraint.dataType,
        value: attributeFormFields.valueConstraint.value ? attributeFormFields.valueConstraint.value : null,
        valueList:
          attributeFormFields.valueConstraint.valueList.length > 0
            ? attributeFormFields.valueConstraint.valueList.map((item) => item.value)
            : null,
        pattern: attributeFormFields.valueConstraint.pattern ? attributeFormFields.valueConstraint.pattern : null,
        minValue: attributeFormFields.valueConstraint.minValue
          ? Number(attributeFormFields.valueConstraint.minValue)
          : null,
        maxValue: attributeFormFields.valueConstraint.maxValue
          ? Number(attributeFormFields.valueConstraint.maxValue)
          : null,
        minCount: attributeFormFields.valueConstraint.requireValue ? 1 : 0,
        maxCount: null,
      }
    : null,
});

export const createEmptyAttributeFormFields = (): AttributeFormFields => ({
  base: {
    name: "",
    predicate: null,
    description: "",
  },
  qualifiers: {
    provenance: null,
    range: null,
    regularity: null,
    scope: null,
  },
  unitRequirement: UnitRequirement.NoUnit,
  units: [],
  valueConstraint: getNotSetValueConstraintFields(),
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
