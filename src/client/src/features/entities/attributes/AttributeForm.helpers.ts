import { useParams } from "react-router-dom";
import {
  useCreateAttribute,
  useGetAttribute,
  useUpdateAttribute,
} from "../../../external/sources/attribute/attribute.queries";
import { FormMode } from "../types/formMode";
import { AttributeView } from "common/types/attributes/attributeView";
import { ValueConstraintRequest } from "common/types/attributes/valueConstraintRequest";
import { AttributeTypeRequest } from "common/types/attributes/attributeTypeRequest";
import { RdlPredicate } from "common/types/attributes/rdlPredicate";
import { InfoItem } from "common/types/infoItem";
import { ConstraintType } from "common/types/attributes/constraintType";
import { ValueObject } from "../types/valueObject";
import { RdlUnit } from "common/types/attributes/rdlUnit";
import { XsdDataType } from "common/types/attributes/xsdDataType";

export const useAttributeQuery = () => {
  const { id } = useParams();
  return useGetAttribute(id);
};

export const useAttributeMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateAttribute();
  const updateMutation = useUpdateAttribute(id);
  return mode === "edit" ? updateMutation : createMutation;
};

export interface AttributeFormFields
  extends Omit<AttributeTypeRequest, "description" | "unitIds" | "unitMinCount" | "unitMaxCount" | "valueConstraint"> {
  description: string;
  units: RdlUnit[];
  unitRequirement: UnitRequirements;
  valueConstraint: boolean;
  constraintType: ConstraintType | undefined;
  dataType: XsdDataType | undefined;
  requireValue: boolean | undefined;
  value: string | undefined;
  valueList: ValueObject<string>[];
  pattern: string | undefined;
  minValue: string | undefined;
  maxValue: string | undefined;
}

export enum UnitRequirements {
  NoUnit = 0,
  Optional = 1,
  Required = 2,
}

export const toAttributeFormFields = (attribute: AttributeView): AttributeFormFields => ({
  name: attribute.name,
  description: attribute.description ?? "",
  predicateId: attribute.predicate?.id,
  units: attribute.units,
  unitRequirement:
    attribute.unitMinCount === 1
      ? UnitRequirements.Required
      : attribute.unitMaxCount === 1
      ? UnitRequirements.Optional
      : UnitRequirements.NoUnit,
  provenanceQualifier: attribute.provenanceQualifier,
  rangeQualifier: attribute.rangeQualifier,
  regularityQualifier: attribute.regularityQualifier,
  scopeQualifier: attribute.scopeQualifier,
  valueConstraint: !!attribute.valueConstraint,
  constraintType: attribute.valueConstraint?.constraintType,
  dataType: attribute.valueConstraint?.dataType,
  requireValue: attribute.valueConstraint?.minCount ? attribute.valueConstraint.minCount > 0 : undefined,
  value: attribute.valueConstraint?.value?.toString(),
  valueList: attribute.valueConstraint?.valueList?.map((x) => ({ value: x.toString() })) ?? [],
  pattern: attribute.valueConstraint?.pattern,
  minValue: attribute.valueConstraint?.minValue?.toString(),
  maxValue: attribute.valueConstraint?.maxValue?.toString(),
});

export const toAttributeTypeRequest = (attributeFormFields: AttributeFormFields): AttributeTypeRequest => ({
  name: attributeFormFields.name,
  description: attributeFormFields.description.length === 0 ? undefined : attributeFormFields.description,
  predicateId: attributeFormFields.predicateId,
  unitIds: attributeFormFields.units.map((x) => x.id),
  unitMinCount: attributeFormFields.unitRequirement === UnitRequirements.Required ? 1 : 0,
  unitMaxCount: attributeFormFields.unitRequirement === UnitRequirements.NoUnit ? 0 : 1,
  provenanceQualifier: attributeFormFields.provenanceQualifier,
  rangeQualifier: attributeFormFields.rangeQualifier,
  regularityQualifier: attributeFormFields.regularityQualifier,
  scopeQualifier: attributeFormFields.scopeQualifier,
  valueConstraint: toValueConstraintRequest(attributeFormFields),
});

export const toValueConstraintRequest = (
  attributeFormFields: AttributeFormFields,
): ValueConstraintRequest | undefined => {
  if (!attributeFormFields.valueConstraint) return undefined;

  return {
    constraintType: attributeFormFields.constraintType ?? ConstraintType.HasSpecificValue,
    dataType: attributeFormFields.dataType ?? XsdDataType.String,
    minCount: attributeFormFields.requireValue
      ? 1
      : attributeFormFields.constraintType === ConstraintType.HasSpecificValue
      ? undefined
      : 0,
    maxCount: undefined,
    value: attributeFormFields.value,
    valueList: attributeFormFields.valueList.map((x) => x.value),
    pattern: attributeFormFields.pattern,
    minValue: attributeFormFields.minValue ? Number(attributeFormFields.minValue) : undefined,
    maxValue: attributeFormFields.maxValue ? Number(attributeFormFields.maxValue) : undefined,
  };
};

export const createDefaultAttributeFormFields = (): AttributeFormFields => ({
  name: "",
  description: "",
  predicateId: undefined,
  units: [],
  unitRequirement: UnitRequirements.NoUnit,
  provenanceQualifier: undefined,
  rangeQualifier: undefined,
  regularityQualifier: undefined,
  scopeQualifier: undefined,
  valueConstraint: false,
  constraintType: undefined,
  dataType: undefined,
  requireValue: undefined,
  value: undefined,
  valueList: [],
  pattern: undefined,
  minValue: undefined,
  maxValue: undefined,
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
