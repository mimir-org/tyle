import { useCreateAttribute, useGetAttribute, useUpdateAttribute } from "api/attribute.queries";
import { useParams } from "react-router-dom";
import { AttributeTypeRequest } from "types/attributes/attributeTypeRequest";
import { AttributeView } from "types/attributes/attributeView";
import { ConstraintType } from "types/attributes/constraintType";
import { RdlPredicate } from "types/attributes/rdlPredicate";
import { RdlUnit } from "types/attributes/rdlUnit";
import { ValueConstraintRequest } from "types/attributes/valueConstraintRequest";
import { XsdDataType } from "types/attributes/xsdDataType";
import { FormMode } from "types/formMode";
import { InfoItem } from "types/infoItem";
import { ValueObject } from "types/valueObject";
import { UnitRequirement } from "./UnitRequirement";

export const useAttributeQuery = () => {
  const { id } = useParams();
  return useGetAttribute(id ?? "");
};

export const useAttributeMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateAttribute();
  const updateMutation = useUpdateAttribute(id ?? "");
  return mode === "edit" ? updateMutation : createMutation;
};

export interface AttributeFormFields
  extends Omit<AttributeTypeRequest, "predicateId" | "unitIds" | "unitMinCount" | "unitMaxCount" | "valueConstraint"> {
  predicate?: RdlPredicate;
  units?: RdlUnit[];
  unitRequirement: UnitRequirement;
  valueConstraint: boolean;
  constraintType?: ConstraintType;
  dataType?: XsdDataType;
  requireValue: boolean;
  value?: string;
  valueList?: ValueObject<string>[];
  pattern?: string;
  minValue?: string;
  maxValue?: string;
}

export const toAttributeFormFields = (attribute: AttributeView): AttributeFormFields => ({
  name: attribute.name,
  description: attribute.description,
  predicate: attribute.predicate,
  units: attribute.units.length === 0 ? undefined : attribute.units,
  unitRequirement:
    attribute.unitMinCount === 1
      ? UnitRequirement.Required
      : attribute.unitMaxCount === 1
        ? UnitRequirement.Optional
        : UnitRequirement.NoUnit,
  provenanceQualifier: attribute.provenanceQualifier,
  rangeQualifier: attribute.rangeQualifier,
  regularityQualifier: attribute.regularityQualifier,
  scopeQualifier: attribute.scopeQualifier,
  valueConstraint: !!attribute.valueConstraint,
  constraintType: attribute.valueConstraint?.constraintType,
  dataType: attribute.valueConstraint?.dataType,
  requireValue: attribute.valueConstraint?.minCount ? attribute.valueConstraint.minCount > 0 : false,
  value: attribute.valueConstraint?.value?.toString(),
  valueList: attribute.valueConstraint?.valueList?.map((x) => ({ value: x.toString() })),
  pattern: attribute.valueConstraint?.pattern,
  minValue: attribute.valueConstraint?.minValue?.toString(),
  maxValue: attribute.valueConstraint?.maxValue?.toString(),
});

export const toAttributeTypeRequest = (attributeFormFields: AttributeFormFields): AttributeTypeRequest => ({
  name: attributeFormFields.name,
  description: attributeFormFields.description ? attributeFormFields.description : undefined,
  predicateId: attributeFormFields.predicate?.id,
  unitIds: attributeFormFields.units ? attributeFormFields.units.map((x) => x.id) : [],
  unitMinCount: attributeFormFields.unitRequirement === UnitRequirement.Required ? 1 : 0,
  unitMaxCount: attributeFormFields.unitRequirement === UnitRequirement.NoUnit ? 0 : 1,
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
    valueList: attributeFormFields.valueList ? attributeFormFields.valueList.map((x) => x.value) : [],
    pattern: attributeFormFields.pattern,
    minValue: attributeFormFields.minValue ? Number(attributeFormFields.minValue) : undefined,
    maxValue: attributeFormFields.maxValue ? Number(attributeFormFields.maxValue) : undefined,
  };
};

export const createDefaultAttributeFormFields = (): AttributeFormFields => ({
  name: "",
  unitRequirement: UnitRequirement.NoUnit,
  valueConstraint: false,
  requireValue: false,
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
