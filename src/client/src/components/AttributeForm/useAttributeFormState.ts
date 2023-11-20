import React from "react";
import { RdlUnit } from "types/attributes/rdlUnit";
import {
  AttributeBaseFields,
  AttributeFormFields,
  AttributeQualifierFields,
  createEmptyAttributeFormFields,
} from "./AttributeForm.helpers";
import { UnitRequirement } from "./UnitRequirement";

export const useAttributeFormState = (): [
  attributeFormFields: AttributeFormFields,
  setAttributeFormFields: (nextAttributeFormFields: AttributeFormFields) => void,
  setBaseFields: (nextBaseFields: AttributeBaseFields) => void,
  setQualifiers: (nextQualifiers: AttributeQualifierFields) => void,
  setUnitRequirement: (nextUnitRequirement: UnitRequirement) => void,
  setUnits: (nextUnits: RdlUnit[]) => void,
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

  return [attributeFormFields, setAttributeFormFields, setBaseFields, setQualifiers, setUnitRequirement, setUnits];
};
