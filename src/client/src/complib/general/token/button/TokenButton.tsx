import { TokenBaseProps } from "complib/general/token/Token";
import { MotionTokenContainer } from "complib/general/token/Token.styled";
import { Text } from "complib/text";
import { ButtonHTMLAttributes, ForwardedRef, forwardRef } from "react";
import { useTheme } from "styled-components";

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
  const theme = useTheme();
  const { children, ...delegated } = props;

  return (
    <MotionTokenContainer ref={ref} as={"button"} $interactive {...theme.mimirorg.animation.buttonTap} {...delegated}>
      <Text variant={"label-small"} useEllipsis ellipsisMaxLines={1}>
        {children}
      </Text>
    </MotionTokenContainer>
  );
});

TokenButton.displayName = "TokenButton";
