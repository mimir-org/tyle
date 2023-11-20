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
import { ValueConstraintRequest } from "types/attributes/valueConstraintRequest";
import { FormMode } from "types/formMode";
import { InfoItem } from "types/infoItem";
import { UnitRequirement } from "./UnitRequirement";

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
  valueConstraint: ValueConstraintRequest | null;
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
        ...attributeView.valueConstraint,
        value: attributeView.valueConstraint.value?.toString() ?? null,
        valueList: attributeView.valueConstraint.valueList?.map((value) => value.toString()) ?? null,
      }
    : null,
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
  valueConstraint: attributeFormFields.valueConstraint,
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
