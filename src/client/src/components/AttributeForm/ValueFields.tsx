import { FormField, Input } from "@mimirorg/component-library";
import { VALUE_LENGTH } from "types/common/stringLengthConstants";
import { BooleanValueFieldset, BooleanValueLegend } from "./ValueFields.styled";

interface ValueFieldProps {
  value: string;
  setValue: (value: string) => void;
}

export const StringValueField = ({ value, setValue }: ValueFieldProps) => {
  return (
    <FormField label="Value">
      <Input
        required={true}
        maxLength={VALUE_LENGTH}
        value={value}
        onChange={(event) => setValue(event.target.value)}
      />
    </FormField>
  );
};

export const DecimalValueField = ({ value, setValue }: ValueFieldProps) => {
  return (
    <FormField label="Value">
      <Input
        required={true}
        pattern="^-?([0-9]+\.)?[0-9]+$"
        title="Enter a valid decimal number, e.g. -12.34"
        value={value}
        onChange={(event) => setValue(event.target.value)}
      />
    </FormField>
  );
};

export const IntegerValueField = ({ value, setValue }: ValueFieldProps) => {
  return (
    <FormField label="Value">
      <Input
        required={true}
        pattern="^-?[0-9]+$"
        title="Enter a valid integer, e.g. -32"
        value={value}
        onChange={(event) => setValue(event.target.value)}
      />
    </FormField>
  );
};

export const BooleanValueField = ({ value, setValue }: ValueFieldProps) => {
  return (
    <BooleanValueFieldset>
      <BooleanValueLegend>Value</BooleanValueLegend>
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
    </BooleanValueFieldset>
  );
};
