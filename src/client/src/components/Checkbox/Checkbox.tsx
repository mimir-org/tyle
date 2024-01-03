import { CheckboxProps } from "@radix-ui/react-checkbox";
import { ForwardedRef, forwardRef } from "react";
import { useTheme } from "styled-components";
import { CheckboxChecked, CheckboxEmpty, CheckboxIndicator, MotionCheckboxRoot } from "./Checkbox.styled";

/**
 * A simple checkbox wrapper, with styling that follows library conventions.
 *
 * @see https://www.radix-ui.com/docs/primitives/components/checkbox
 *
 * @constructor
 */
const Checkbox = forwardRef((props: CheckboxProps, ref: ForwardedRef<HTMLButtonElement>) => {
  const theme = useTheme();

  return (
    <MotionCheckboxRoot ref={ref} {...theme.tyle.animation.checkboxTap} {...props}>
      <CheckboxEmpty />
      <CheckboxIndicator>
        <CheckboxChecked />
      </CheckboxIndicator>
    </MotionCheckboxRoot>
  );
});

Checkbox.displayName = "Checkbox";

export default Checkbox;
