import { RadioGroupItem, RadioGroupItemProps } from "@radix-ui/react-radio-group";
import { Text } from "complib/text";
import { ForwardedRef, forwardRef } from "react";
import { useTheme } from "styled-components";
import { MotionTokenContainer } from "../Token.styled";

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
          {...theme.mimirorg.animation.radioButtonTap}
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
