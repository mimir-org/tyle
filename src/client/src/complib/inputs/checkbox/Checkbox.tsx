import { CheckboxProps } from "@radix-ui/react-checkbox";
import { ForwardedRef, forwardRef } from "react";
import { useTheme } from "styled-components";
import { CheckboxEmptyIcon } from "./assets";
import { CheckboxChecked, CheckboxIndicator, MotionCheckboxRoot } from "./Checkbox.styled";

/**
 * A simple checkbox wrapper, with styling that follows library conventions.
 *
 * For documentation about the underlying checkbox component see the link below.
 * @see https://www.radix-ui.com/docs/primitives/components/checkbox
 *
 * @constructor
 */
export const Checkbox = forwardRef((props: CheckboxProps, ref: ForwardedRef<HTMLButtonElement>) => {
  const theme = useTheme();

  return (
    <MotionCheckboxRoot ref={ref} {...theme.tyle.animation.checkboxTap} {...props}>
      <CheckboxEmptyIcon />
      <CheckboxIndicator>
        <CheckboxChecked />
      </CheckboxIndicator>
    </MotionCheckboxRoot>
  );
});

Checkbox.displayName = "Checkbox";
