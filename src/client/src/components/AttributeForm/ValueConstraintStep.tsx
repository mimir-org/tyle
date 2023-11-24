import { Box, Flexbox, FormField, Select } from "@mimirorg/component-library";
import Switch from "components/Switch";
import React from "react";
import { useTheme } from "styled-components";
import { ConstraintType } from "types/attributes/constraintType";
import { XsdDataType } from "types/attributes/xsdDataType";
import { getOptionsFromEnum } from "utils";
import { FormStepProps } from "./AttributeForm";
import {
  NumberRangeFields,
  PatternFields,
  SpecificBooleanValueFields,
  SpecificStringOrNumericalValueFields,
  ValueListFields,
  ValueListItem,
} from "./ConditionalValueConstraintFields";

const ValueConstraintStep = React.forwardRef<HTMLFormElement, FormStepProps>(({ fields, setFields }, ref) => {
  const theme = useTheme();

  const [enabled, setEnabled] = React.useState(!!fields.valueConstraint);
  const [requireValue, setRequireValue] = React.useState(
    fields.valueConstraint ? fields.valueConstraint.minCount > 0 : false,
  );
  const [constraintType, setConstraintType] = React.useState(
    fields.valueConstraint?.constraintType ?? ConstraintType.HasSpecificValue,
  );
  const [dataType, setDataType] = React.useState(fields.valueConstraint?.dataType ?? XsdDataType.String);
  const [value, setValue] = React.useState(fields.valueConstraint?.value?.toString() ?? "");
  const [valueList, setValueList] = React.useState<ValueListItem[]>(
    fields.valueConstraint?.valueList?.map((value) => ({ id: crypto.randomUUID(), value: value.toString() })) ?? [],
  );
  const [pattern, setPattern] = React.useState(fields.valueConstraint?.pattern ?? "");
  const [minValue, setMinValue] = React.useState(fields.valueConstraint?.minValue?.toString() ?? "");
  const [maxValue, setMaxValue] = React.useState(fields.valueConstraint?.maxValue?.toString() ?? "");

  const constraintTypeOptions = getOptionsFromEnum<ConstraintType>(ConstraintType);
  const handleConstraintTypeChange = (nextConstraintType: ConstraintType) => {
    if (nextConstraintType === constraintType) return;

    if (constraintType === ConstraintType.IsInListOfAllowedValues && dataType === XsdDataType.Boolean) {
      setDataType(XsdDataType.String);
    } else if (constraintType === ConstraintType.MatchesRegexPattern) {
      setDataType(XsdDataType.String);
    } else if (
      constraintType === ConstraintType.IsInNumberRange &&
      dataType !== XsdDataType.Integer &&
      dataType !== XsdDataType.Decimal
    ) {
      setDataType(XsdDataType.Decimal);
    }

    setConstraintType(nextConstraintType);
    resetAllValueFields();
  };

  const resetAllValueFields = () => {
    setValue("");
    setValueList([]);
    setPattern("");
    setMinValue("");
    setMaxValue("");
  };

  const getDataTypeOptions = () => {
    const options = getOptionsFromEnum<XsdDataType>(XsdDataType);

    if (constraintType === ConstraintType.IsInListOfAllowedValues) {
      return options.filter((option) => option.value !== XsdDataType.Boolean);
    }

    if (constraintType === ConstraintType.MatchesRegexPattern) {
      return options.filter((option) => option.value === XsdDataType.String);
    }

    if (constraintType === ConstraintType.IsInNumberRange) {
      return options.filter((option) => option.value === XsdDataType.Decimal || option.value === XsdDataType.Integer);
    }

    return options;
  };
  const dataTypeOptions = getDataTypeOptions();

  const getConditionalValueConstraintFields = (constraintType: ConstraintType, dataType: XsdDataType) => {
    switch (constraintType) {
      case ConstraintType.HasSpecificValue:
        if (dataType === XsdDataType.Boolean) {
          return <SpecificBooleanValueFields value={value} setValue={setValue} />;
        }
        return <SpecificStringOrNumericalValueFields value={value} setValue={setValue} />;
      case ConstraintType.IsInListOfAllowedValues:
        return <ValueListFields valueList={valueList} setValueList={setValueList} />;
      case ConstraintType.MatchesRegexPattern:
        return <PatternFields value={pattern} setValue={setPattern} />;
      case ConstraintType.IsInNumberRange:
        return (
          <NumberRangeFields
            minValue={minValue}
            maxValue={maxValue}
            setMinValue={setMinValue}
            setMaxValue={setMaxValue}
          />
        );
      default:
        return <></>;
    }
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setFields({
      ...fields,
      valueConstraint: enabled
        ? {
            constraintType,
            dataType,
            value:
              dataType === XsdDataType.Boolean
                ? value === "true"
                : dataType === XsdDataType.Decimal || dataType === XsdDataType.Integer
                  ? Number(value)
                  : value,
            valueList:
              dataType === XsdDataType.Decimal || dataType === XsdDataType.Integer
                ? valueList.map((item) => Number(item.value))
                : valueList.map((item) => item.value),
            pattern,
            minValue: minValue ? Number(minValue) : null,
            maxValue: maxValue ? Number(maxValue) : null,
            minCount: requireValue ? 1 : 0,
            maxCount: null,
          }
        : null,
    });
  };

  return (
    <form onSubmit={handleSubmit} ref={ref}>
      <Box maxWidth="50rem">
        <Flexbox gap={theme.mimirorg.spacing.xl} flexDirection="column">
          <Flexbox justifyContent="space-between">
            <Flexbox alignItems="center" gap={theme.mimirorg.spacing.l}>
              <Box>Add value constraint</Box>
              <Switch checked={enabled} onCheckedChange={(checked) => setEnabled(checked)} />
            </Flexbox>
            {enabled && constraintType !== ConstraintType.HasSpecificValue && (
              <Flexbox alignItems="center" gap={theme.mimirorg.spacing.l}>
                <Box>Require value to be set</Box>
                <Switch checked={requireValue} onCheckedChange={(checked) => setRequireValue(checked)} />
              </Flexbox>
            )}
          </Flexbox>
          {enabled && (
            <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl}>
              <Box flexGrow="1">
                <FormField label="Constraint type">
                  <Select
                    placeholder="Select a constraint type"
                    options={constraintTypeOptions}
                    onChange={(x) => {
                      if (x?.value !== undefined) handleConstraintTypeChange(x.value);
                    }}
                    value={constraintTypeOptions.find((x) => x.value === constraintType)}
                  />
                </FormField>
              </Box>
              <Box flexGrow="1">
                <FormField label="Data type">
                  <Select
                    placeholder="Select a data type"
                    options={dataTypeOptions}
                    onChange={(x) => setDataType(x?.value ?? XsdDataType.String)}
                    value={dataTypeOptions.find((x) => x.value === dataType)}
                  />
                </FormField>
              </Box>
            </Flexbox>
          )}
          {enabled && getConditionalValueConstraintFields(constraintType, dataType)}
        </Flexbox>
      </Box>
    </form>
  );
});

ValueConstraintStep.displayName = "ValueConstraintStep";

export default ValueConstraintStep;
