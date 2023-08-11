import { CheckboxProps } from "@radix-ui/react-checkbox";
import { CheckboxEmptyIcon } from "complib/inputs/checkbox/assets";
import { CheckboxChecked, CheckboxIndicator, MotionCheckboxRoot } from "complib/inputs/checkbox/Checkbox.styled";
import { ForwardedRef, forwardRef } from "react";
import { useTheme } from "styled-components";

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
    <MotionCheckboxRoot ref={ref} {...theme.mimirorg.animation.checkboxTap} {...props}>
      <CheckboxEmptyIcon />
      <CheckboxIndicator>
        <CheckboxChecked />
      </CheckboxIndicator>
    </MotionCheckboxRoot>
  );
});

Checkbox.displayName = "Checkbox";
