import { FormField, Input, Select } from "@mimirorg/component-library";
import { PlusCircle, XCircle } from "@styled-icons/heroicons-outline";
import Switch from "components/Switch";
import React from "react";
import { ConstraintType } from "types/attributes/constraintType";
import { XsdDataType } from "types/attributes/xsdDataType";
import { VALUE_LENGTH } from "types/common/stringLengthConstants";
import { getOptionsFromEnum } from "utils";
import { AttributeFormStepProps } from "./AttributeForm";
import {
  AddValueListItemWrapper,
  ConstraintTypeSelectionWrapper,
  HasSpecificBooleanValueFieldset,
  RangeFieldsWrapper,
  ValueConstraintStepHeader,
  ValueConstraintStepWrapper,
  ValueListFieldsWrapper,
  ValueListItemsWrapper,
} from "./ValueConstraintStep.styled";

const ValueConstraintStep = React.forwardRef<HTMLFormElement, AttributeFormStepProps>(({ fields, setFields }, ref) => {
  const [enabled, setEnabled] = React.useState(!!fields.valueConstraint);
  const [requireValue, setRequireValue] = React.useState(
    fields.valueConstraint ? fields.valueConstraint.minCount > 0 : false,
  );
  const [constraintType, setConstraintType] = React.useState(
    fields.valueConstraint?.constraintType ?? ConstraintType.HasSpecificValue,
  );
  const [dataType, setDataType] = React.useState(fields.valueConstraint?.dataType ?? XsdDataType.String);
  const [value, setValue] = React.useState(fields.valueConstraint?.value?.toString() ?? "");
  const [valueList, setValueList] = React.useState(
    fields.valueConstraint?.valueList?.map((value) => ({ id: crypto.randomUUID(), value: value.toString() })) ?? [],
  );
  const [pattern, setPattern] = React.useState(fields.valueConstraint?.pattern ?? "");
  const [minValue, setMinValue] = React.useState(fields.valueConstraint?.minValue?.toString() ?? "");
  const [maxValue, setMaxValue] = React.useState(fields.valueConstraint?.maxValue?.toString() ?? "");

  const valueListRef = React.useRef<(HTMLInputElement | null)[]>([]);
  React.useEffect(() => {
    if (constraintType === ConstraintType.IsInListOfAllowedValues && valueList.length === 0) {
      setValueList([{ id: crypto.randomUUID(), value: "" }]);
    }
  }, [constraintType, valueList.length]);

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

  const valueListContains = (value: string) => {
    if (!value) return true;

    if (dataType !== XsdDataType.String) {
      const numericalValueList = valueList.map((item) => Number(item.value));
      return numericalValueList.includes(Number(value));
    }

    return valueList.map((item) => item.value).includes(value);
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
      case ConstraintType.HasSpecificValue:
        if (dataType === XsdDataType.Boolean) {
          return (
            <HasSpecificBooleanValueFieldset>
              <legend>Value</legend>
              <label htmlFor="has-specific-value-true">
                <input
                  type="radio"
                  required={true}
                  id="has-specific-value-true"
                  name="has-specific-boolean-value"
                  value={"true"}
                  checked={value === "true"}
                  onChange={() => setValue("true")}
                />{" "}
                True
              </label>
              <label htmlFor="has-specific-value-false">
                <input
                  type="radio"
                  id="has-specific-value-false"
                  name="has-specific-boolean-value"
                  value={"false"}
                  checked={value === "false"}
                  onChange={() => setValue("false")}
                />{" "}
                False
              </label>
            </HasSpecificBooleanValueFieldset>
          );
        }
        return (
          <FormField label="Value">
            <Input
              required={true}
              maxLength={VALUE_LENGTH}
              {...valueInputValidation}
              value={value}
              onChange={(event) => setValue(event.target.value)}
            />
          </FormField>
        );
      case ConstraintType.IsInListOfAllowedValues:
        return (
          <ValueListFieldsWrapper>
            {valueList.map((item, index) => (
              <ValueListItemsWrapper key={item.id}>
                <Input
                  required={true}
                  maxLength={VALUE_LENGTH}
                  {...valueInputValidation}
                  value={item.value}
                  onChange={(event) => {
                    if (valueListContains(event.target.value)) {
                      valueListRef.current[index]?.setCustomValidity("Value is not unique");
                    } else {
                      valueListRef.current[index]?.setCustomValidity("");
                    }

                    const nextValueList = [...valueList];
                    nextValueList[index] = { id: valueList[index].id, value: event.target.value };
                    setValueList(nextValueList);
                  }}
                  ref={(element) => (valueListRef.current[index] = element)}
                />
                {valueList.length > 1 && (
                  <XCircle
                    onClick={() => {
                      setValueList(valueList.filter((x) => x.id !== item.id));
                    }}
                  />
                )}
              </ValueListItemsWrapper>
            ))}
            <AddValueListItemWrapper>
              <PlusCircle onClick={() => setValueList([...valueList, { id: crypto.randomUUID(), value: "" }])} />
            </AddValueListItemWrapper>
          </ValueListFieldsWrapper>
        );
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

    setFields({
      ...fields,
      valueConstraint: enabled
        ? {
            constraintType,
            dataType,
            value: value ? value : null,
            valueList: valueList.map((item) => item.value),
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
    <ValueConstraintStepWrapper onSubmit={handleSubmit} ref={ref}>
      <ValueConstraintStepHeader>
        <Switch checked={enabled} onCheckedChange={(checked) => setEnabled(checked)}>
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
      {enabled && getConditionalValueConstraintFields(constraintType, dataType)}
    </ValueConstraintStepWrapper>
  );
});

ValueConstraintStep.displayName = "ValueConstraintStep";

export default ValueConstraintStep;
