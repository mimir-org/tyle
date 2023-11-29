import { FormField, Input } from "@mimirorg/component-library";
import React from "react";
import { XsdDataType } from "types/attributes/xsdDataType";
import { RangeFieldsWrapper } from "./RangeValueFields.styled";

interface RangeFieldsProps {
  minValue: string;
  maxValue: string;
  setMinValue: (minValue: string) => void;
  setMaxValue: (maxValue: string) => void;
  dataType: XsdDataType;
}

export const RangeValueFields = ({ minValue, maxValue, setMinValue, setMaxValue, dataType }: RangeFieldsProps) => {
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
    <RangeFieldsWrapper>
      <FormField label="Lower bound (leave empty for no lower bound)">
        <Input {...minValueValidation} value={minValue} onChange={handleMinValueChange} ref={minValueRef} />
      </FormField>
      <FormField label="Upper bound (leave empty for no upper bound)">
        <Input {...maxValueValidation} value={maxValue} onChange={handleMaxValueChange} />
      </FormField>
    </RangeFieldsWrapper>
  );
};
