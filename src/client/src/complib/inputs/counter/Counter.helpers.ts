import { NumberProps } from "complib/inputs/counter/Counter";
import { useEffect } from "react";

export const useOnChangeCallback = (onChange: NumberProps["onChange"], value: number) =>
  useEffect(() => {
    onChange && onChange(value);
  }, [onChange, value]);
