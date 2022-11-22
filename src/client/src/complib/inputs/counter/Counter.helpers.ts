import { NumberProps } from "complib/inputs/counter/Counter";
import { useEffect } from "react";

export const useOnChangeCallback = (onChange: NumberProps["onChange"], value: number) =>
  useEffect(() => {
    onChange && onChange(value);
  }, [onChange, value]);

export const onChangeCounterValue = (
  value: number,
  onChangeCallback: (value: number) => void,
  options: { min?: number; max?: number }
) => {
  const { min, max } = options;

  if (max != null && value > max) return;
  if (min != null && value < min) return;
  onChangeCallback(value);
};
