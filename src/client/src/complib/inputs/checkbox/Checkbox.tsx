import { CheckboxProps } from "@radix-ui/react-checkbox";
import { ForwardedRef, forwardRef } from "react";
import { CheckboxEmptyIcon } from "../../../assets/icons/checkmark";
import { CheckboxChecked, CheckboxIndicator, CheckboxRoot } from "./Checkbox.styled";

/**
 * A simple checkbox wrapper, with styling that follows library conventions.
 *
 * For documentation about the underlying checkbox component see the link below.
 * @see https://www.radix-ui.com/docs/primitives/components/checkbox
 *
 * @constructor
 */
export const Checkbox = forwardRef((props: CheckboxProps, ref: ForwardedRef<HTMLButtonElement>) => (
  <CheckboxRoot ref={ref} {...props}>
    <CheckboxEmptyIcon />
    <CheckboxIndicator>
      <CheckboxChecked />
    </CheckboxIndicator>
  </CheckboxRoot>
));

Checkbox.displayName = "Checkbox";
