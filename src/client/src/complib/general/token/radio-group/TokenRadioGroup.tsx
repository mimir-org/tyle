import { RadioGroupProps } from "@radix-ui/react-radio-group";
import { TokenRadioGroupRoot } from "complib/general/token/radio-group/TokenRadioGroup.styled";

export type TokenRadioGroupProps = Omit<RadioGroupProps, "asChild">;

/**
 * A radio group wrapper, with styling that follows library conventions.
 *
 * For documentation about the underlying checkbox component see the link below.
 * @see https://www.radix-ui.com/docs/primitives/components/radio-group
 *
 * @constructor
 */
export const TokenRadioGroup = ({ children, ...delegated }: TokenRadioGroupProps) => (
  <TokenRadioGroupRoot {...delegated}>{children}</TokenRadioGroupRoot>
);
