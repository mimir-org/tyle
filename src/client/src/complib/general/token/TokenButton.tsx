import { ButtonHTMLAttributes, ForwardedRef, forwardRef } from "react";
import { Text } from "../../text";
import { TokenBaseProps } from "./Token";
import { TokenContainer } from "./Token.styled";

export type TokenButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & Omit<TokenBaseProps, "interactive">;

/**
 * A button wrapper for the Token component.
 * Exposes standard button attributes to the consumer.
 *
 * @param children text to be displayed inside token
 * @param variant controls style of the token
 * @constructor
 */
export const TokenButton = forwardRef((props: TokenButtonProps, ref: ForwardedRef<HTMLButtonElement>) => {
  const { children, ...delegated } = props;

  return (
    <TokenContainer ref={ref} as={"button"} $interactive {...delegated}>
      <Text variant={"label-small"} useEllipsis ellipsisMaxLines={1}>
        {children}
      </Text>
    </TokenContainer>
  );
});

TokenButton.displayName = "TokenButton";
