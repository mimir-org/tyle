import { FormField } from "@mimirorg/component-library";
import Input from "components/Input";
import React from "react";
import { XsdDataType } from "types/attributes/xsdDataType";
import { RangeValueFieldsWrapper } from "./RangeValueFields.styled";

interface RangeValueFieldsProps {
  minValue: string;
  maxValue: string;
  setMinValue: (minValue: string) => void;
  setMaxValue: (maxValue: string) => void;
  dataType: XsdDataType;
}

const RangeValueFields = ({ minValue, maxValue, setMinValue, setMaxValue, dataType }: RangeValueFieldsProps) => {
  const minValueRef = React.useRef<HTMLInputElement>(null);

  const numberValidation =
    dataType === XsdDataType.Decimal
      ? { pattern: "^-?([0-9]+.)?[0-9]+$", title: "Enter a valid decimal number, e.g. -12.34" }
      : { pattern: "^-?[0-9]+$", title: "Enter a valid integer, e.g. -32" };

  const minValueValidation = maxValue ? numberValidation : { ...numberValidation, required: true };
  const maxValueValidation = minValue ? numberValidation : { ...numberValidation, required: true };

  const validateBounds = (minValue: string, maxValue: string) => {
    const minValueNum = minValue ? Number(minValue) : NaN;
    const maxValueNum = maxValue ? Number(maxValue) : NaN;

    if (Number.isNaN(minValueNum) || Number.isNaN(maxValueNum) || minValueNum <= maxValueNum) {
      minValueRef.current?.setCustomValidity("");
    } else {
      minValueRef.current?.setCustomValidity("The lower bound can't be larger than the upper bound.");
    }
  };

  const handleMinValueChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const nextMinValue = event.target.value;
    validateBounds(nextMinValue, maxValue);
    setMinValue(nextMinValue);
  };

  const handleMaxValueChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const nextMaxValue = event.target.value;
    validateBounds(minValue, nextMaxValue);
    setMaxValue(nextMaxValue);
  };

  return (
    <RangeValueFieldsWrapper>
      <FormField label="Lower bound (leave empty for no lower bound)">
        <Input {...minValueValidation} value={minValue} onChange={handleMinValueChange} ref={minValueRef} />
      </FormField>
      <FormField label="Upper bound (leave empty for no upper bound)">
        <Input {...maxValueValidation} value={maxValue} onChange={handleMaxValueChange} />
      </FormField>
    </RangeValueFieldsWrapper>
  );
};

export default RangeValueFields;
