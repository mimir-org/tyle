import React from "react";
import { RdlUnit } from "types/attributes/rdlUnit";
import {
  AttributeBaseFields,
  AttributeFormFields,
  AttributeQualifierFields,
  UnitRequirement,
  createEmptyAttributeFormFields,
} from "./AttributeForm.helpers";
import { ValueConstraintFields } from "./ValueConstraintStep.helpers";

export const useAttributeFormState = (): [
  attributeFormFields: AttributeFormFields,
  setAttributeFormFields: (nextAttributeFormFields: AttributeFormFields) => void,
  setBaseFields: (nextBaseFields: AttributeBaseFields) => void,
  setQualifiers: (nextQualifiers: AttributeQualifierFields) => void,
  setUnitRequirement: (nextUnitRequirement: UnitRequirement) => void,
  setUnits: (nextUnits: RdlUnit[]) => void,
  setValueConstraint: (nextValueConstraint: ValueConstraintFields) => void,
] => {
  const [attributeFormFields, setAttributeFormFields] = React.useState(createEmptyAttributeFormFields);

  const setBaseFields = (nextBaseFields: AttributeBaseFields) => {
    setAttributeFormFields({ ...attributeFormFields, base: nextBaseFields });
  };

  const setQualifiers = (nextQualifiers: AttributeQualifierFields) => {
    setAttributeFormFields({ ...attributeFormFields, qualifiers: nextQualifiers });
  };

  const setUnitRequirement = (nextUnitRequirement: UnitRequirement) => {
    setAttributeFormFields({ ...attributeFormFields, unitRequirement: nextUnitRequirement });
  };

  const setUnits = (nextUnits: RdlUnit[]) => {
    setAttributeFormFields({ ...attributeFormFields, units: nextUnits });
  };

  const setValueConstraint = (nextValueConstraint: ValueConstraintFields) => {
    setAttributeFormFields({ ...attributeFormFields, valueConstraint: nextValueConstraint });
  };

  return [
    attributeFormFields,
    setAttributeFormFields,
    setBaseFields,
    setQualifiers,
    setUnitRequirement,
    setUnits,
    setValueConstraint,
  ];
};
