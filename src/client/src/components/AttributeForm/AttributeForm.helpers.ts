import { useCreateAttribute, useUpdateAttribute } from "api/attribute.queries";
import { AttributeTypeRequest } from "types/attributes/attributeTypeRequest";
import { AttributeView } from "types/attributes/attributeView";
import { ProvenanceQualifier } from "types/attributes/provenanceQualifier";
import { RangeQualifier } from "types/attributes/rangeQualifier";
import { RdlPredicate } from "types/attributes/rdlPredicate";
import { RdlUnit } from "types/attributes/rdlUnit";
import { RegularityQualifier } from "types/attributes/regularityQualifier";
import { ScopeQualifier } from "types/attributes/scopeQualifier";
import { ValueConstraintRequest } from "types/attributes/valueConstraintRequest";
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
  valueConstraint: ValueConstraintRequest | null;
}

export enum UnitRequirement {
  NoUnit = 0,
  Optional = 1,
  Required = 2,
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
        ...attributeView.valueConstraint,
        value: attributeView.valueConstraint.value?.toString() ?? null,
        valueList: attributeView.valueConstraint.valueList?.map((item) => item.toString()) ?? [],
      }
    : null,
});

export const toAttributeTypeRequest = (attributeFormFields: AttributeFormFields): AttributeTypeRequest => ({
  ...attributeFormFields,
  description: attributeFormFields.description ? attributeFormFields.description : null,
  predicateId: attributeFormFields.predicate?.id ?? null,
  unitIds: attributeFormFields.units.map((unit) => unit.id),
  unitMinCount: attributeFormFields.unitRequirement === UnitRequirement.Required ? 1 : 0,
  unitMaxCount: attributeFormFields.unitRequirement === UnitRequirement.NoUnit ? 0 : 1,
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
  valueConstraint: null,
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
