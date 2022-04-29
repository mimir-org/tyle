import { ButtonContainer, ButtonContainerProps } from "./Button.styled";
import { Text } from "../text";
import { VisuallyHidden } from "../accessibility";
import { Icon } from "../media";

interface ButtonProps extends ButtonContainerProps {
  children: string;
  leftIcon?: string;
  rightIcon?: string;
  iconOnly?: boolean;
}

export const Button = ({ children, leftIcon, rightIcon, iconOnly, ...delegated }: ButtonProps) => (
  <ButtonContainer {...delegated}>
    {leftIcon && <Icon size={18} src={leftIcon} alt="" />}
    {iconOnly ? <VisuallyHidden>{children}</VisuallyHidden> : <Text variant={"label-large"}>{children}</Text>}
    {rightIcon && <Icon size={18} src={rightIcon} alt="" />}
  </ButtonContainer>
);
