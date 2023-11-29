import { FormField, Select } from "@mimirorg/component-library";
import Switch from "components/Switch";
import React from "react";
import { ConstraintType } from "types/attributes/constraintType";
import { XsdDataType } from "types/attributes/xsdDataType";
import { getOptionsFromEnum } from "utils";
import { AttributeFormStepProps } from "./AttributeForm";
import { RangeValueFields } from "./RangeValueFields";
import {
  ConstraintTypeSelectionWrapper,
  ValueConstraintStepHeader,
  ValueConstraintStepWrapper,
} from "./ValueConstraintStep.styled";
import { BooleanValueField, DecimalValueField, IntegerValueField, StringValueField } from "./ValueFields";
import { ValueListFields } from "./ValueListFields";

const ValueConstraintStep = React.forwardRef<HTMLFormElement, AttributeFormStepProps>(({ fields, setFields }, ref) => {
  const { enabled, requireValue, constraintType, dataType, value, valueList, pattern, minValue, maxValue } =
    fields.valueConstraint;

  const setEnabled = (enabled: boolean) =>
    setFields((fields) => ({ ...fields, valueConstraint: { ...fields.valueConstraint, enabled } }));
  const setRequireValue = (requireValue: boolean) =>
    setFields((fields) => ({ ...fields, valueConstraint: { ...fields.valueConstraint, requireValue } }));
  const setConstraintType = (constraintType: ConstraintType) =>
    setFields((fields) => ({ ...fields, valueConstraint: { ...fields.valueConstraint, constraintType } }));
  const setDataType = (dataType: XsdDataType) =>
    setFields((fields) => ({ ...fields, valueConstraint: { ...fields.valueConstraint, dataType } }));
  const setValue = (value: string) =>
    setFields((fields) => ({ ...fields, valueConstraint: { ...fields.valueConstraint, value } }));
  const setValueList = (valueList: { id: string; value: string }[]) =>
    setFields((fields) => ({ ...fields, valueConstraint: { ...fields.valueConstraint, valueList } }));
  const setPattern = (pattern: string) =>
    setFields((fields) => ({ ...fields, valueConstraint: { ...fields.valueConstraint, pattern } }));
  const setMinValue = (minValue: string) =>
    setFields((fields) => ({ ...fields, valueConstraint: { ...fields.valueConstraint, minValue } }));
  const setMaxValue = (maxValue: string) =>
    setFields((fields) => ({ ...fields, valueConstraint: { ...fields.valueConstraint, maxValue } }));

  const constraintTypeOptions = getOptionsFromEnum<ConstraintType>(ConstraintType);
  const handleConstraintTypeChange = (nextConstraintType: ConstraintType) => {
    if (nextConstraintType === constraintType) return;

    if (nextConstraintType === ConstraintType.IsInListOfAllowedValues && dataType === XsdDataType.Boolean) {
      handleDataTypeChange(XsdDataType.String);
    } else if (nextConstraintType === ConstraintType.MatchesRegexPattern) {
      handleDataTypeChange(XsdDataType.String);
    } else if (
      nextConstraintType === ConstraintType.IsInNumberRange &&
      dataType !== XsdDataType.Integer &&
      dataType !== XsdDataType.Decimal
    ) {
      handleDataTypeChange(XsdDataType.Decimal);
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
  const handleDataTypeChange = (nextDataType: XsdDataType) => {
    if (nextDataType === dataType) return;

    resetAllValueFields();

    setDataType(nextDataType);
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
  };

  return (
    <ValueConstraintStepWrapper onSubmit={handleSubmit} ref={ref}>
      <ValueConstraintStepHeader>
        <Switch
          checked={enabled}
          onCheckedChange={(checked) => {
            setEnabled(checked);
            resetAllValueFields();
          }}
        >
          Add value constraint
        </Switch>
        {enabled && constraintType !== ConstraintType.HasSpecificValue && (
          <Switch checked={requireValue} onCheckedChange={(checked) => setRequireValue(checked)}>
            Require value to be set
          </Switch>
        )}
      </ValueConstraintStepHeader>
      {enabled && (
        <ConstraintTypeSelectionWrapper>
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
          <FormField label="Data type">
            <Select
              placeholder="Select a data type"
              options={dataTypeOptions}
              onChange={(x) => {
                if (x?.value !== undefined) handleDataTypeChange(x.value);
              }}
              value={dataTypeOptions.find((x) => x.value === dataType)}
            />
          </FormField>
        </ConstraintTypeSelectionWrapper>
      )}
      {enabled && constraintType === ConstraintType.HasSpecificValue && dataType === XsdDataType.String && (
        <StringValueField value={value} setValue={setValue} />
      )}
      {enabled && constraintType === ConstraintType.HasSpecificValue && dataType === XsdDataType.Decimal && (
        <DecimalValueField value={value} setValue={setValue} />
      )}
      {enabled && constraintType === ConstraintType.HasSpecificValue && dataType === XsdDataType.Integer && (
        <IntegerValueField value={value} setValue={setValue} />
      )}
      {enabled && constraintType === ConstraintType.HasSpecificValue && dataType === XsdDataType.Boolean && (
        <BooleanValueField value={value} setValue={setValue} />
      )}
      {enabled && constraintType === ConstraintType.IsInListOfAllowedValues && (
        <ValueListFields valueList={valueList} setValueList={setValueList} dataType={dataType} />
      )}
      {enabled && constraintType === ConstraintType.MatchesRegexPattern && (
        <StringValueField value={pattern} setValue={setPattern} label="Pattern" />
      )}
      {enabled && constraintType === ConstraintType.IsInNumberRange && (
        <RangeValueFields
          minValue={minValue}
          maxValue={maxValue}
          setMinValue={setMinValue}
          setMaxValue={setMaxValue}
          dataType={dataType}
        />
      )}
    </ValueConstraintStepWrapper>
  );
});

ValueConstraintStep.displayName = "ValueConstraintStep";

export default ValueConstraintStep;
