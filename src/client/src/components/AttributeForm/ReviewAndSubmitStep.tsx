import { Button, Flexbox, PlainLink, Table, Tbody, Td, Tr } from "@mimirorg/component-library";
import { useTheme } from "styled-components";
import { ConstraintType } from "types/attributes/constraintType";
import { ProvenanceQualifier } from "types/attributes/provenanceQualifier";
import { RangeQualifier } from "types/attributes/rangeQualifier";
import { RegularityQualifier } from "types/attributes/regularityQualifier";
import { ScopeQualifier } from "types/attributes/scopeQualifier";
import { XsdDataType } from "types/attributes/xsdDataType";
import { FormMode } from "types/formMode";
import { addSpacesToPascalCase } from "utils";
import { AttributeFormFields, UnitRequirement } from "./AttributeForm.helpers";
import { ValueConstraintFields } from "./ValueConstraintStep.helpers";

interface ReviewAndSubmitProps {
  attributeFormFields: AttributeFormFields;
  mode?: FormMode;
}

const getValueConstraintText = (valueConstraint: ValueConstraintFields) => {
  if (!valueConstraint.set) return "not set";

  switch (valueConstraint.constraintType) {
    case ConstraintType.HasSpecificValue:
      return `Has the value ${valueConstraint.value}`;
    case ConstraintType.IsInListOfAllowedValues:
      return `Has${
        valueConstraint.requireValue ? " " : " no value or "
      }one of the following values: ${valueConstraint.valueList.map((x) => x.value).join(", ")}`;
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

const ReviewAndSubmitStep = ({ attributeFormFields, mode }: ReviewAndSubmitProps) => {
  const theme = useTheme();

  const getQualifiersString = () => {
    const qualifierNames: string[] = [];
    const { provenance, range, regularity, scope } = attributeFormFields.qualifiers;

    if (provenance !== null) qualifierNames.push(ProvenanceQualifier[provenance].replace("Qualifier", ""));
    if (range !== null) qualifierNames.push(RangeQualifier[range].replace("Qualifier", ""));
    if (regularity !== null) qualifierNames.push(RegularityQualifier[regularity].replace("Qualifier", ""));
    if (scope !== null) qualifierNames.push(ScopeQualifier[scope].replace("Qualifier", ""));

    return qualifierNames.join(" / ");
  };

  return (
    <Flexbox gap={theme.mimirorg.spacing.xl} flexDirection="column">
      <Table>
        <Tbody>
          <Tr>
            <Td>Name</Td>
            <Td>{attributeFormFields.base.name}</Td>
          </Tr>
          <Tr>
            <Td>Predicate</Td>
            <Td>{attributeFormFields.base.predicate ? attributeFormFields.base.predicate.name : "undefined"}</Td>
          </Tr>
          <Tr>
            <Td>Description</Td>
            <Td>{attributeFormFields.base.description}</Td>
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

      {!attributeFormFields.base.name && <p style={{ color: "red" }}>Required field name is missing</p>}

      <Flexbox gap={theme.mimirorg.spacing.xl}>
        <Button type="submit" disabled={!attributeFormFields.base.name}>
          {mode === "edit" ? "Save changes" : "Create new type"}
        </Button>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            Cancel
          </Button>
        </PlainLink>
      </Flexbox>
    </Flexbox>
  );
};

export default ReviewAndSubmitStep;
