import { ButtonContainer, ButtonContainerProps } from "./Button.styled";
import { Text } from "../text";
import { VisuallyHidden } from "../accessibility";
import { Icon } from "../media";
import { forwardRef } from "react";

interface ButtonProps extends ButtonContainerProps {
  children: string;
  leftIcon?: string;
  rightIcon?: string;
  iconOnly?: boolean;
}

export const Button = forwardRef<HTMLButtonElement, ButtonProps>(
  ({ children, leftIcon, rightIcon, iconOnly, ...delegated }, ref) => (
    <ButtonContainer ref={ref} {...delegated}>
      {leftIcon && <Icon size={18} src={leftIcon} alt="" />}
      {iconOnly ? <VisuallyHidden>{children}</VisuallyHidden> : <Text variant={"label-large"}>{children}</Text>}
      {rightIcon && <Icon size={18} src={rightIcon} alt="" />}
    </ButtonContainer>
  )
);

Button.displayName = "ButtonWF";
