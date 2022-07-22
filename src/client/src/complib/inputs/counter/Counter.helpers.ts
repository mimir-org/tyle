import { useEffect } from "react";
import { NumberProps } from "./Counter";

export const useOnChangeCallback = (onChange: NumberProps["onChange"], value: number) =>
  useEffect(() => {
    onChange && onChange(value);
  }, [onChange, value]);
