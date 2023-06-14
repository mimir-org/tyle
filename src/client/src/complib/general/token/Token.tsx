import { Button } from "complib/buttons";
import { TokenContainer } from "complib/general/token/Token.styled";
import { Text } from "complib/text";
import { Actionable } from "complib/types";
import { ForwardedRef, forwardRef, HTMLAttributes, ReactNode } from "react";

export type TokenBaseProps = {
  children?: ReactNode;
  variant?: "primary" | "secondary";
  $interactive?: boolean;
  $selected?: boolean;
};

export type TokenProps = HTMLAttributes<HTMLSpanElement> & TokenBaseProps & Partial<Actionable>;

/**
 * A component for representing a piece of data.
 * Often used to display a collection of related attributes.
 *
 * The interactive and selected prop are transient, read more about this in the documentation link below.
 * @see https://styled-components.com/docs/api#transient-props
 *
 * @param children text to be displayed inside token
 * @param variant controls style of the token
 * @param $interactive enables interaction styles for token
 * @param $selected enables selected styles for token
 * @param actionable
 * @param actionIcon
 * @param actionText
 * @param onAction
 * @constructor
 */
export const Token = forwardRef((props: TokenProps, ref: ForwardedRef<HTMLSpanElement>) => {
  const { children, actionable, actionIcon, actionText, onAction, dangerousAction, ...delegated } = props;

  return (
    <TokenContainer ref={ref} {...delegated}>
      <Text variant={"label-small"} useEllipsis ellipsisMaxLines={1}>
        {children}
      </Text>
      {actionable && onAction && (
        <Button variant={"text"} onClick={onAction} icon={actionIcon} iconOnly dangerousAction={dangerousAction}>
          {actionText}
        </Button>
      )}
    </TokenContainer>
  );
});

Token.displayName = "Token";
