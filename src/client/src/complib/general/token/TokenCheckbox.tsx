import { CheckboxProps } from "@radix-ui/react-checkbox";
import { ForwardedRef, forwardRef } from "react";
import { Text } from "../../text";
import { TokenBaseProps } from "./Token";
import { TokenCheckboxContainer } from "./TokenCheckbox.styled";

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
  const { children, ...delegated } = props;

  return (
    <TokenCheckboxContainer ref={ref} $interactive {...delegated}>
      <Text variant={"label-small"} useEllipsis ellipsisMaxLines={1}>
        {children}
      </Text>
    </TokenCheckboxContainer>
  );
});

TokenCheckbox.displayName = "TokenCheckbox";
