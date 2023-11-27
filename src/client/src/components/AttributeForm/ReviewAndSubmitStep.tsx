import { Button, PlainLink, Table, Tbody, Td, Tr } from "@mimirorg/component-library";
import { ConstraintType } from "types/attributes/constraintType";
import { ProvenanceQualifier } from "types/attributes/provenanceQualifier";
import { RangeQualifier } from "types/attributes/rangeQualifier";
import { RegularityQualifier } from "types/attributes/regularityQualifier";
import { ScopeQualifier } from "types/attributes/scopeQualifier";
import { ValueConstraintRequest } from "types/attributes/valueConstraintRequest";
import { XsdDataType } from "types/attributes/xsdDataType";
import { FormMode } from "types/formMode";
import { addSpacesToPascalCase } from "utils";
import { AttributeFormFields, UnitRequirement } from "./AttributeForm.helpers";
import { ReviewAndSubmitStepWrapper, SubmitButtonsWrapper } from "./ReviewAndSubmitStep.styled";

interface ReviewAndSubmitProps {
  attributeFormFields: AttributeFormFields;
  mode?: FormMode;
}

const getValueConstraintText = (valueConstraint: ValueConstraintRequest | null) => {
  if (!valueConstraint) return "not set";

  switch (valueConstraint.constraintType) {
    case ConstraintType.HasSpecificValue:
      return `Has the value ${valueConstraint.value}`;
    case ConstraintType.IsInListOfAllowedValues:
      return `Has${
        valueConstraint.minCount > 0 ? " " : " no value or "
      }one of the following values: ${valueConstraint.valueList.join(", ")}`;
    case ConstraintType.HasSpecificDataType:
      return `Has${valueConstraint.minCount > 0 ? " " : " no value or "}datatype ${XsdDataType[
        valueConstraint.dataType
      ].toLowerCase()}`;
    case ConstraintType.MatchesRegexPattern:
      return `${valueConstraint.minCount > 0 ? "M" : "Has no value or m"}atches the regex pattern ${
        valueConstraint.pattern
      }`;
    case ConstraintType.IsInNumberRange:
      if (!valueConstraint.minValue) {
        return `${valueConstraint.minCount > 0 ? "I" : "Has no value or i"}s lower than or equal to ${
          valueConstraint.maxValue
        }`;
      }
      if (!valueConstraint.maxValue) {
        return `${valueConstraint.minCount > 0 ? "I" : "Has no value or i"}s higher than or equal to ${
          valueConstraint.minValue
        }`;
      }
      return `${valueConstraint.minCount > 0 ? "I" : "Has no value or i"}s between ${valueConstraint.minValue} and ${
        valueConstraint.maxValue
      }`;
  }
};

const ReviewAndSubmitStep = ({ attributeFormFields, mode }: ReviewAndSubmitProps) => {
  const getQualifiersString = () => {
    const qualifierNames: string[] = [];

    if (attributeFormFields.provenanceQualifier !== null)
      qualifierNames.push(ProvenanceQualifier[attributeFormFields.provenanceQualifier].replace("Qualifier", ""));
    if (attributeFormFields.rangeQualifier !== null)
      qualifierNames.push(RangeQualifier[attributeFormFields.rangeQualifier].replace("Qualifier", ""));
    if (attributeFormFields.regularityQualifier !== null)
      qualifierNames.push(RegularityQualifier[attributeFormFields.regularityQualifier].replace("Qualifier", ""));
    if (attributeFormFields.scopeQualifier !== null)
      qualifierNames.push(ScopeQualifier[attributeFormFields.scopeQualifier].replace("Qualifier", ""));

    return qualifierNames.join(" / ");
  };

  return (
    <ReviewAndSubmitStepWrapper>
      <Table>
        <Tbody>
          <Tr>
            <Td>Name</Td>
            <Td>{attributeFormFields.name}</Td>
          </Tr>
          <Tr>
            <Td>Predicate</Td>
            <Td>{attributeFormFields.predicate ? attributeFormFields.predicate.name : "undefined"}</Td>
          </Tr>
          <Tr>
            <Td>Description</Td>
            <Td>{attributeFormFields.description}</Td>
          </Tr>
          <Tr>
            <Td>Qualifiers</Td>
            <Td>{getQualifiersString()}</Td>
          </Tr>
          <Tr>
            <Td>Unit requirement</Td>
            <Td>{addSpacesToPascalCase(UnitRequirement[attributeFormFields.unitRequirement])}</Td>
          </Tr>
          <Tr>
            <Td>Units</Td>
            <Td>
              {attributeFormFields.units.length > 0
                ? attributeFormFields.units
                    .map((unit) => unit.name + (unit.symbol ? ` (${unit.symbol})` : ""))
                    .join(", ")
                : "none"}
            </Td>
          </Tr>
          <Tr>
            <Td>Value constraint</Td>
            <Td>{getValueConstraintText(attributeFormFields.valueConstraint)}</Td>
          </Tr>
        </Tbody>
      </Table>

      <SubmitButtonsWrapper>
        <Button type="submit">{mode === "edit" ? "Save changes" : "Create new type"}</Button>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            Cancel
          </Button>
        </PlainLink>
      </SubmitButtonsWrapper>
    </ReviewAndSubmitStepWrapper>
  );
};

export default ReviewAndSubmitStep;
