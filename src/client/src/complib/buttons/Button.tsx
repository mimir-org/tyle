import { ButtonContainer, ButtonContainerProps } from "./Button.styled";
import { Text } from "../text";
import { VisuallyHidden } from "../accessibility";
import { Icon } from "../media";
import { forwardRef, isValidElement, ReactElement, ReactNode } from "react";

type ButtonProps = ButtonContainerProps & {
  children: ReactNode;
  icon?: string | ReactElement;
  iconPlacement?: "left" | "right";
  iconOnly?: boolean;
};

export const Button = forwardRef<HTMLButtonElement, ButtonProps>(
  ({ children, icon, iconPlacement, iconOnly, ...delegated }, ref) => {
    const IconComponent = () => (isValidElement(icon) ? icon : <Icon src={icon} alt="" />);

    return (
      <ButtonContainer ref={ref} iconPlacement={iconPlacement} iconOnly={iconOnly} {...delegated}>
        {icon && iconOnly ? (
          <VisuallyHidden>{children}</VisuallyHidden>
        ) : (
          <Text as={"span"} variant={"title-medium"}>
            {children}
          </Text>
        )}
        {icon && <IconComponent />}
      </ButtonContainer>
    );
  }
);

Button.displayName = "Button";
Button.defaultProps = {
  iconPlacement: "right",
};
