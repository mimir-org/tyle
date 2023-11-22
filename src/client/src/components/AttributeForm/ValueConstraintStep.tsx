import { Box, Flexbox, FormField, Select } from "@mimirorg/component-library";
import Switch from "components/Switch";
import { useTheme } from "styled-components";
import { ConstraintType } from "types/attributes/constraintType";
import { XsdDataType } from "types/attributes/xsdDataType";
import { getOptionsFromEnum } from "utils";
import {
  NumberRangeFields,
  PatternFields,
  SpecificBooleanValueFields,
  SpecificStringOrNumericalValueFields,
  ValueListFields,
} from "./ConditionalValueConstraintFields";
import { ValueConstraintFields, ValueListItem, getEmptyValueFields } from "./ValueConstraintStep.helpers";

interface ValueConstraintStepProps {
  valueConstraint: ValueConstraintFields;
  setValueConstraint: (nextValueConstraint: ValueConstraintFields) => void;
}

const ValueConstraintStep = ({ valueConstraint, setValueConstraint }: ValueConstraintStepProps) => {
  const theme = useTheme();

  const constraintTypeOptions = getOptionsFromEnum<ConstraintType>(ConstraintType);
  const handleConstraintTypeChange = (constraintType: ConstraintType) => {
    if (constraintType === valueConstraint.constraintType) return;

    let dataType = valueConstraint.dataType;

    if (constraintType === ConstraintType.IsInListOfAllowedValues && dataType === XsdDataType.Boolean) {
      dataType = XsdDataType.String;
    } else if (constraintType === ConstraintType.MatchesRegexPattern) {
      dataType = XsdDataType.String;
    } else if (
      constraintType === ConstraintType.IsInNumberRange &&
      dataType !== XsdDataType.Integer &&
      dataType !== XsdDataType.Decimal
    ) {
      dataType = XsdDataType.Decimal;
    }

    setValueConstraint({ ...valueConstraint, constraintType, dataType, ...getEmptyValueFields() });
  };

  const getDataTypeOptions = () => {
    const options = getOptionsFromEnum<XsdDataType>(XsdDataType);

    if (valueConstraint.constraintType === ConstraintType.IsInListOfAllowedValues) {
      return options.filter((option) => option.value !== XsdDataType.Boolean);
    }

    if (valueConstraint.constraintType === ConstraintType.MatchesRegexPattern) {
      return options.filter((option) => option.value === XsdDataType.String);
    }

    if (valueConstraint.constraintType === ConstraintType.IsInNumberRange) {
      return options.filter((option) => option.value === XsdDataType.Decimal || option.value === XsdDataType.Integer);
    }

    return options;
  };
  const dataTypeOptions = getDataTypeOptions();

  const handleSetChange = (checked: boolean) => {
    setValueConstraint({ ...valueConstraint, set: checked });
  };

  const handleRequireValueChange = (checked: boolean) => {
    setValueConstraint({ ...valueConstraint, requireValue: checked });
  };

  const handleSpecificValueChange = (nextValue: string) => {
    setValueConstraint({ ...valueConstraint, value: nextValue });
  };

  const handleValueListChange = (nextValueList: ValueListItem[]) => {
    setValueConstraint({ ...valueConstraint, valueList: nextValueList });
  };

  const handlePatternChange = (nextPattern: string) => {
    setValueConstraint({ ...valueConstraint, pattern: nextPattern });
  };

  const handleNumberRangeChange = (nextMinValue: string, nextMaxValue: string) => {
    setValueConstraint({ ...valueConstraint, minValue: nextMinValue, maxValue: nextMaxValue });
  };

  const getConditionalValueConstraintFields = (constraintType: ConstraintType, dataType: XsdDataType) => {
    switch (constraintType) {
      case ConstraintType.HasSpecificValue:
        if (dataType === XsdDataType.Boolean) {
          return <SpecificBooleanValueFields value={valueConstraint.value} setValue={handleSpecificValueChange} />;
        }
        return (
          <SpecificStringOrNumericalValueFields value={valueConstraint.value} setValue={handleSpecificValueChange} />
        );
      case ConstraintType.IsInListOfAllowedValues:
        return <ValueListFields valueList={valueConstraint.valueList} setValueList={handleValueListChange} />;
      case ConstraintType.MatchesRegexPattern:
        return <PatternFields value={valueConstraint.pattern} setValue={handlePatternChange} />;
      case ConstraintType.IsInNumberRange:
        return (
          <NumberRangeFields
            minValue={valueConstraint.minValue}
            maxValue={valueConstraint.maxValue}
            setNumberRange={handleNumberRangeChange}
          />
        );
      default:
        return <></>;
    }
  };

  return (
    <Box maxWidth="50rem">
      <Flexbox gap={theme.mimirorg.spacing.xl} flexDirection="column">
        <Flexbox justifyContent="space-between">
          <Flexbox alignItems="center" gap={theme.mimirorg.spacing.l}>
            <Box>Add value constraint</Box>
            <Switch checked={valueConstraint.set} onCheckedChange={(checked) => handleSetChange(checked)} />
          </Flexbox>
          {valueConstraint.set && valueConstraint.constraintType !== ConstraintType.HasSpecificValue && (
            <Flexbox alignItems="center" gap={theme.mimirorg.spacing.l}>
              <Box>Require value to be set</Box>
              <Switch
                checked={valueConstraint.requireValue}
                onCheckedChange={(checked) => handleRequireValueChange(checked)}
              />
            </Flexbox>
          )}
        </Flexbox>
        {valueConstraint.set && (
          <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl}>
            <Box flexGrow="1">
              <FormField label="Constraint type">
                <Select
                  placeholder="Select a constraint type"
                  options={constraintTypeOptions}
                  onChange={(x) => {
                    if (x?.value !== undefined) handleConstraintTypeChange(x.value);
                  }}
                  value={constraintTypeOptions.find((x) => x.value === valueConstraint?.constraintType)}
                />
              </FormField>
            </Box>
            <Box flexGrow="1">
              <FormField label="Data type">
                <Select
                  placeholder="Select a data type"
                  options={dataTypeOptions}
                  onChange={(x) => {
                    setValueConstraint({
                      ...valueConstraint,
                      dataType: x?.value ?? XsdDataType.String,
                    });
                  }}
                  value={dataTypeOptions.find((x) => x.value === valueConstraint?.dataType)}
                />
              </FormField>
            </Box>
          </Flexbox>
        )}
        {valueConstraint.set &&
          getConditionalValueConstraintFields(valueConstraint.constraintType, valueConstraint.dataType)}
      </Flexbox>
    </Box>
  );
};

export default ValueConstraintStep;
