import { CheckboxProps } from "@radix-ui/react-checkbox";
import { ForwardedRef, forwardRef } from "react";
import { useTheme } from "styled-components";
import { Text } from "../../text";
import { TokenBaseProps } from "./Token";
import { MotionTokenCheckboxContainer } from "./TokenCheckbox.styled";

export type TokenCheckboxProps = CheckboxProps & Omit<TokenBaseProps, "interactive">;

/**
 * A checkbox wrapper for the Token component.
 *
 * For documentation about the underlying checkbox component see the link below.
 * @see https://www.radix-ui.com/docs/primitives/components/checkbox
 *
 * @param children text to be displayed inside token
 * @param variant controls style of the token
 * @constructor
 */
export const TokenCheckbox = forwardRef((props: TokenCheckboxProps, ref: ForwardedRef<HTMLButtonElement>) => {
  const theme = useTheme();
  const { children, ...delegated } = props;

  return (
    <MotionTokenCheckboxContainer ref={ref} $interactive {...theme.tyle.animation.checkboxTap} {...delegated}>
      <Text variant={"label-small"} useEllipsis ellipsisMaxLines={1}>
        {children}
      </Text>
    </MotionTokenCheckboxContainer>
  );
});

TokenCheckbox.displayName = "TokenCheckbox";
