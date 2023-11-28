import { FormField, Input, Select } from "@mimirorg/component-library";
import Switch from "components/Switch";
import React from "react";
import { ConstraintType } from "types/attributes/constraintType";
import { XsdDataType } from "types/attributes/xsdDataType";
import { VALUE_LENGTH } from "types/common/stringLengthConstants";
import { getOptionsFromEnum } from "utils";
import { AttributeFormStepProps } from "./AttributeForm";
import {
  ConstraintTypeSelectionWrapper,
  RangeFieldsWrapper,
  ValueConstraintStepHeader,
  ValueConstraintStepWrapper,
} from "./ValueConstraintStep.styled";
import { BooleanValueField, DecimalValueField, IntegerValueField, StringValueField } from "./ValueFields";
import { DecimalValueListFields, IntegerValueListFields, StringValueListFields } from "./ValueListFields";

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

  const valueListRef = React.useRef<(HTMLInputElement | null)[]>([]);

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

  const getConditionalValueConstraintFields = (constraintType: ConstraintType, dataType: XsdDataType) => {
    const valueInputValidation =
      dataType === XsdDataType.Decimal
        ? { type: "number", step: "any" }
        : dataType === XsdDataType.Integer
          ? { type: "number" }
          : {};

    const minValueValidation = maxValue ? { max: maxValue } : { required: true };
    const maxValueValidation = minValue ? { min: minValue } : { required: true };

    switch (constraintType) {
      case ConstraintType.MatchesRegexPattern:
        return (
          <FormField label="Pattern">
            <Input
              required={true}
              maxLength={VALUE_LENGTH}
              value={pattern}
              onChange={(event) => setPattern(event.target.value)}
            />
          </FormField>
        );
      case ConstraintType.IsInNumberRange:
        return (
          <RangeFieldsWrapper>
            <FormField label="Lower bound (leave empty for no lower bound)">
              <Input
                type="number"
                step={dataType === XsdDataType.Integer ? 1 : "any"}
                {...minValueValidation}
                value={minValue}
                onChange={(event) => setMinValue(event.target.value)}
              />
            </FormField>
            <FormField label="Upper bound (leave empty for no upper bound)">
              <Input
                type="number"
                step={dataType === XsdDataType.Integer ? 1 : "any"}
                {...maxValueValidation}
                value={maxValue}
                onChange={(event) => setMaxValue(event.target.value)}
              />
            </FormField>
          </RangeFieldsWrapper>
        );
      default:
        return <></>;
    }
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
      {enabled && constraintType === ConstraintType.IsInListOfAllowedValues && dataType === XsdDataType.String && (
        <StringValueListFields valueList={valueList} setValueList={setValueList} valueListRef={valueListRef} />
      )}
      {enabled && constraintType === ConstraintType.IsInListOfAllowedValues && dataType === XsdDataType.Decimal && (
        <DecimalValueListFields valueList={valueList} setValueList={setValueList} valueListRef={valueListRef} />
      )}
      {enabled && constraintType === ConstraintType.IsInListOfAllowedValues && dataType === XsdDataType.Integer && (
        <IntegerValueListFields valueList={valueList} setValueList={setValueList} valueListRef={valueListRef} />
      )}
    </ValueConstraintStepWrapper>
  );
});

ValueConstraintStep.displayName = "ValueConstraintStep";

export default ValueConstraintStep;
