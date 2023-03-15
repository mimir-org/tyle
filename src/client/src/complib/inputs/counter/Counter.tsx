import { MinusSmall, PlusSmall } from "@styled-icons/heroicons-outline";
import { Button } from "complib/buttons";
import { onChangeCounterValue, useOnChangeCallback } from "complib/inputs/counter/Counter.helpers";
import { CounterContainer, CounterInput } from "complib/inputs/counter/Counter.styled";
import { Sizing } from "complib/props";
import { ForwardedRef, forwardRef, useEffect, useState } from "react";

export type NumberProps = Sizing & {
  id?: string;
  name?: string;
  value: number;
  min?: number;
  max?: number;
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
 * @param min minimum allowed value inclusive
 * @param max maximum allowed value inclusive
 * @param onChange called with current value on change
 * @param disabled disables number input and associated buttons when true
 * @param decreaseText property for overriding the default text for decrease button
 * @param increaseText property for overriding the default text for increase button
 * @param delegated receives sizing props for overriding default styles
 * @constructor
 */
export const Counter = forwardRef((props: NumberProps, ref: ForwardedRef<HTMLInputElement>) => {
  const { id, name, value, min, max, onChange, disabled, decreaseText, increaseText, ...delegated } = props;
  const [fieldValue, setFieldValue] = useState(value);

  useEffect(() => onChangeCounterValue(value, setFieldValue, { min, max }), [max, min, value]);
  useOnChangeCallback(onChange, fieldValue);

  return (
    <CounterContainer {...delegated} disabled={disabled}>
      <CounterInput
        ref={ref}
        id={id}
        name={name}
        type={"number"}
        value={fieldValue}
        disabled={disabled}
        min={min}
        max={max}
        onChange={(e) => onChangeCounterValue(parseFloat(e.target.value), setFieldValue, { min, max })}
      />

      <Button
        order={"-1"}
        tabIndex={-1}
        onClick={() => onChangeCounterValue(fieldValue - 1, setFieldValue, { min, max })}
        variant={"text"}
        icon={<MinusSmall size={24} />}
        iconOnly
        disabled={disabled}
      >
        {decreaseText ?? "Decrease value"}
      </Button>

      <Button
        tabIndex={-1}
        onClick={() => onChangeCounterValue(fieldValue + 1, setFieldValue, { min, max })}
        variant={"text"}
        icon={<PlusSmall size={24} />}
        iconOnly
        disabled={disabled}
      >
        {increaseText ?? "Increase value"}
      </Button>
    </CounterContainer>
  );
});

Counter.displayName = "Number";
