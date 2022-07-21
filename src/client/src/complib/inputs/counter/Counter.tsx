import { MinusSm, PlusSm } from "@styled-icons/heroicons-outline";
import { ForwardedRef, forwardRef, useEffect, useState } from "react";
import { Button } from "../../buttons";
import { Sizing } from "../../props";
import { useOnChangeCallback } from "./Counter.helpers";
import { CounterContainer, CounterInput } from "./Counter.styled";

export type NumberProps = Sizing & {
  id?: string;
  name?: string;
  value?: number;
  onChange?: (value: number) => void;
  disabled?: boolean;
  increaseText?: string;
  decreaseText?: string;
};

/**
 * Component which displays a number input and buttons for increasing/decreasing its value
 *
 * @param id of input field
 * @param name of input field
 * @param value initial/current value of field
 * @param onChange called with current value on change
 * @param disabled disables number input and associated buttons when true
 * @param decreaseText property for overriding the default text for decrease button
 * @param increaseText property for overriding the default text for increase button
 * @param delegated receives sizing props for overriding default styles
 * @constructor
 */
export const Counter = forwardRef((props: NumberProps, ref: ForwardedRef<HTMLInputElement>) => {
  const { id, name, value, onChange, disabled, decreaseText, increaseText, ...delegated } = props;
  const [fieldValue, setFieldValue] = useState(value ?? 0);

  useEffect(() => {
    value && setFieldValue(value);
  }, [value]);

  useOnChangeCallback(onChange, fieldValue);

  return (
    <CounterContainer {...delegated}>
      <Button
        tabIndex={-1}
        onClick={() => setFieldValue(fieldValue - 1)}
        variant={"text"}
        icon={<MinusSm size={24} />}
        iconOnly
        disabled={disabled}
      >
        {decreaseText ?? "Decrease value"}
      </Button>

      <CounterInput
        ref={ref}
        id={id}
        name={name}
        type={"number"}
        value={fieldValue}
        disabled={disabled}
        onChange={(e) => {
          const numberValue = parseFloat(e.target.value);
          numberValue && setFieldValue(numberValue);
        }}
      />

      <Button
        tabIndex={-1}
        onClick={() => setFieldValue(fieldValue + 1)}
        variant={"text"}
        icon={<PlusSm size={24} />}
        iconOnly
        disabled={disabled}
      >
        {increaseText ?? "Increase value"}
      </Button>
    </CounterContainer>
  );
});

Counter.displayName = "Number";
