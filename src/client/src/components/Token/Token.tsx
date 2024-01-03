import { CheckboxProps, Root as CheckboxRoot } from "@radix-ui/react-checkbox";
import { RadioGroupItem, RadioGroupItemProps, RadioGroupProps } from "@radix-ui/react-radio-group";
import Button from "components/Button";
import Text from "components/Text";
import { ButtonHTMLAttributes, ForwardedRef, HTMLAttributes, ReactNode, forwardRef } from "react";
import { useTheme } from "styled-components";
import { Actionable } from "types/actionable";
import { MotionTokenContainer, TokenContainer, TokenRadioGroupRoot } from "./Token.styled";

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
const Token = forwardRef((props: TokenProps, ref: ForwardedRef<HTMLSpanElement>) => {
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

export default Token;

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
    <MotionTokenContainer ref={ref} as={"button"} $interactive {...theme.tyle.animation.buttonTap} {...delegated}>
      <Text variant={"label-small"} useEllipsis ellipsisMaxLines={1}>
        {children}
      </Text>
    </MotionTokenContainer>
  );
});

TokenButton.displayName = "TokenButton";

export type TokenCheckboxProps = Omit<CheckboxProps, "asChild">;

/**
 * A checkbox wrapper for the Token component.
 *
 * For documentation about the underlying checkbox component see the link below.
 * @see https://www.radix-ui.com/docs/primitives/components/checkbox
 *
 * @param children text to be displayed inside token
 * @constructor
 */
export const TokenCheckbox = forwardRef((props: TokenCheckboxProps, ref: ForwardedRef<HTMLButtonElement>) => {
  const theme = useTheme();
  const { children, ...delegated } = props;

  return (
    <CheckboxRoot asChild {...delegated}>
      <MotionTokenContainer
        as={"button"}
        ref={ref}
        variant={"secondary"}
        $interactive
        {...theme.tyle.animation.checkboxTap}
      >
        <Text variant={"label-small"} useEllipsis ellipsisMaxLines={1}>
          {children}
        </Text>
      </MotionTokenContainer>
    </CheckboxRoot>
  );
});

TokenCheckbox.displayName = "TokenCheckbox";

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

export type TokenRadioGroupItemProps = Omit<RadioGroupItemProps, "asChild">;

/**
 * A radio group item wrapper for the Token component.
 *
 * For documentation about the underlying radio group item component see the link below.
 * @see https://www.radix-ui.com/docs/primitives/components/radio-group#item
 *
 * @param children text to be displayed inside token
 * @constructor
 */
export const TokenRadioGroupItem = forwardRef(
  (props: TokenRadioGroupItemProps, ref: ForwardedRef<HTMLButtonElement>) => {
    const theme = useTheme();
    const { children, ...delegated } = props;

    return (
      <RadioGroupItem asChild {...delegated}>
        <MotionTokenContainer
          as={"button"}
          ref={ref}
          variant={"secondary"}
          $interactive
          {...theme.tyle.animation.radioButtonTap}
        >
          <Text variant={"label-small"} useEllipsis ellipsisMaxLines={1}>
            {children}
          </Text>
        </MotionTokenContainer>
      </RadioGroupItem>
    );
  },
);

TokenRadioGroupItem.displayName = "TokenRadioGroupItem";
