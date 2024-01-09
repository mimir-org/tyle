import { useTheme } from "styled-components";
import Box, { MotionBox } from "components/Box";
import Heading from "../Heading";
import StateBadge from "../StateBadge";
import Divider from "../Divider";
import PanelPropertiesContainer from "./PanelPropertiesContainer";
import PanelSection from "./PanelSection";
import Text from "../Text";
import { AttributeView } from "../../types/attributes/attributeView";
import { ProvenanceQualifier } from "../../types/attributes/provenanceQualifier";
import { getOptionsFromEnum } from "../../utils";
import { RangeQualifier } from "../../types/attributes/rangeQualifier";
import { RegularityQualifier } from "../../types/attributes/regularityQualifier";
import { ScopeQualifier } from "../../types/attributes/scopeQualifier";
import { ConstraintType } from "../../types/attributes/constraintType";
import { XsdDataType } from "../../types/attributes/xsdDataType";
import { UnitRequirement } from "../AttributeForm/AttributeForm.helpers";

interface AttributePanelProps {
  attributeData: AttributeView;
}

const AttributePanel = ({ attributeData }: AttributePanelProps) => {
  const theme = useTheme();
  const getUnitRequirement = () => {
    const { unitMinCount, unitMaxCount } = attributeData;

    return unitMinCount === 1 ? UnitRequirement[UnitRequirement.Required] : unitMaxCount === 1 ? UnitRequirement[UnitRequirement.Optional] : UnitRequirement[UnitRequirement.NoUnit];
  };
  const getQualifiersString = () => {
    const qualifierNames: string[] = [];

    if (attributeData.provenanceQualifier !== null)
      qualifierNames.push(ProvenanceQualifier[attributeData.provenanceQualifier].replace("Qualifier", ""));
    if (attributeData.rangeQualifier !== null)
      qualifierNames.push(RangeQualifier[attributeData.rangeQualifier].replace("Qualifier", ""));
    if (attributeData.regularityQualifier !== null)
      qualifierNames.push(RegularityQualifier[attributeData.regularityQualifier].replace("Qualifier", ""));
    if (attributeData.scopeQualifier !== null)
      qualifierNames.push(ScopeQualifier[attributeData.scopeQualifier].replace("Qualifier", ""));

    return qualifierNames.join(" / ");
  };

  const getValueConstraintText = () => {
    const { valueConstraint } = attributeData;
    if (!valueConstraint) return "not set";

    switch (valueConstraint?.constraintType) {
      case ConstraintType.HasSpecificValue:
        return `Has the value ${valueConstraint.value}`;
      case ConstraintType.IsInListOfAllowedValues:
        return `Has${
          valueConstraint.minCount > 0 ? " " : " no value or "
        }one of the following values: ${valueConstraint.valueList?.map((constraint) => constraint).join(", ")}`;
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

  console.log(attributeData);

  return (
    <MotionBox
      flex={1}
      display={"flex"}
      flexDirection={"column"}
      gap={theme.tyle.spacing.xxxl}
      maxHeight={"100%"}
      overflow={"hidden"}
      {...theme.tyle.animation.fade}
    >
      <Box display={"grid"} rowGap={theme.tyle.spacing.xxl}>
        <Box display={"grid"}>
          <Box gridColumn={"1"}>
            <Heading as={"h2"}>{attributeData.name}</Heading>
          </Box>
          <Box display={"flex"} gridColumn={"2"} justifyContent={"right"} alignItems={"center"}>
            <StateBadge state={attributeData.state} />
          </Box>
        </Box>
        <Divider />
        <PanelPropertiesContainer>
          <PanelSection title={"Predicate"}>
            {attributeData.predicate?.name}
          </PanelSection>
          <PanelSection title={"Description"}>
            <Text>{attributeData.description}</Text>
          </PanelSection>
          <PanelSection title={"Qualifiers"}>
            {getQualifiersString()}
          </PanelSection>
          <PanelSection title={"Unit requirement"}>
            <Text>{getUnitRequirement()}</Text>
          </PanelSection>
          <PanelSection title={"Units"}>
            <Text>{attributeData.units.length > 0 ? attributeData.units.map((unit) => unit.name).join(", ") : "none"}</Text>
          </PanelSection>
          <PanelSection title={"Value constraints"}>
            <Text>{getValueConstraintText()}</Text>
          </PanelSection>
        </PanelPropertiesContainer>
      </Box>
    </MotionBox>
  );
};

export default AttributePanel;
