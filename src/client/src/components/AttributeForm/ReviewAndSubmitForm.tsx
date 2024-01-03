import { PlainLink } from "@mimirorg/component-library";
import { UseMutationResult } from "@tanstack/react-query";
import Button from "components/Button";
import { Table, Tbody, Td, Tr } from "components/Table";
import { onSubmitForm, useSubmissionToast } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import { AttributeTypeRequest } from "types/attributes/attributeTypeRequest";
import { AttributeView } from "types/attributes/attributeView";
import { ConstraintType } from "types/attributes/constraintType";
import { ProvenanceQualifier } from "types/attributes/provenanceQualifier";
import { RangeQualifier } from "types/attributes/rangeQualifier";
import { RegularityQualifier } from "types/attributes/regularityQualifier";
import { ScopeQualifier } from "types/attributes/scopeQualifier";
import { XsdDataType } from "types/attributes/xsdDataType";
import { FormMode } from "types/formMode";
import { addSpacesToPascalCase } from "utils";
import { AttributeFormFields, UnitRequirement, toAttributeTypeRequest } from "./AttributeForm.helpers";
import { ReviewAndSubmitFormWrapper, SubmitButtonsWrapper } from "./ReviewAndSubmitForm.styled";

interface ReviewAndSubmitFormProps {
  attributeFormFields: AttributeFormFields;
  mutation: UseMutationResult<AttributeView, unknown, AttributeTypeRequest, unknown>;
  formRef: React.ForwardedRef<HTMLFormElement>;
  mode?: FormMode;
}

const ReviewAndSubmitForm = ({ attributeFormFields, mutation, formRef, mode }: ReviewAndSubmitFormProps) => {
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast("attribute");

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    onSubmitForm(toAttributeTypeRequest(attributeFormFields), mutation.mutateAsync, toast);
  };

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

  const getValueConstraintText = () => {
    const { valueConstraint } = attributeFormFields;
    if (!valueConstraint.enabled) return "not set";

    switch (valueConstraint.constraintType) {
      case ConstraintType.HasSpecificValue:
        return `Has the value ${valueConstraint.value}`;
      case ConstraintType.IsInListOfAllowedValues:
        return `Has${
          valueConstraint.requireValue ? " " : " no value or "
        }one of the following values: ${valueConstraint.valueList.join(", ")}`;
      case ConstraintType.HasSpecificDataType:
        return `Has${valueConstraint.requireValue ? " " : " no value or "}datatype ${XsdDataType[
          valueConstraint.dataType
        ].toLowerCase()}`;
      case ConstraintType.MatchesRegexPattern:
        return `${valueConstraint.requireValue ? "M" : "Has no value or m"}atches the regex pattern ${
          valueConstraint.pattern
        }`;
      case ConstraintType.IsInNumberRange:
        if (!valueConstraint.minValue) {
          return `${valueConstraint.requireValue ? "I" : "Has no value or i"}s lower than or equal to ${
            valueConstraint.maxValue
          }`;
        }
        if (!valueConstraint.maxValue) {
          return `${valueConstraint.requireValue ? "I" : "Has no value or i"}s higher than or equal to ${
            valueConstraint.minValue
          }`;
        }
        return `${valueConstraint.requireValue ? "I" : "Has no value or i"}s between ${valueConstraint.minValue} and ${
          valueConstraint.maxValue
        }`;
    }
  };

  return (
    <ReviewAndSubmitFormWrapper onSubmit={handleSubmit} ref={formRef}>
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
                ? attributeFormFields.units.map((unit) => unit.name).join(", ")
                : "none"}
            </Td>
          </Tr>
          <Tr>
            <Td>Value constraint</Td>
            <Td>{getValueConstraintText()}</Td>
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
    </ReviewAndSubmitFormWrapper>
  );
};

export default ReviewAndSubmitForm;
