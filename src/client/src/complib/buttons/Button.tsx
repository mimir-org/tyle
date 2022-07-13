import { ForwardedRef, forwardRef, isValidElement, ReactElement, ReactNode } from "react";
import { VisuallyHidden } from "../accessibility";
import { Icon } from "../media";
import { Text } from "../text";
import { ButtonContainer, ButtonContainerProps } from "./Button.styled";

type ButtonProps = ButtonContainerProps & {
  children: ReactNode;
  icon?: string | ReactElement;
  iconPlacement?: "left" | "right";
  iconOnly?: boolean;
};

export const Button = forwardRef((props: ButtonProps, ref: ForwardedRef<HTMLButtonElement>) => {
  const { children, icon, iconPlacement, iconOnly, ...delegated } = props;
  const IconComponent = () => (isValidElement(icon) ? icon : <Icon src={icon} alt="" />);

  return (
    <ButtonContainer ref={ref} iconPlacement={iconPlacement} iconOnly={iconOnly} {...delegated}>
      {icon && iconOnly ? (
        <VisuallyHidden>{children}</VisuallyHidden>
      ) : (
        <Text as={"span"} variant={"body-small"}>
          {children}
        </Text>
      )}
      {icon && <IconComponent />}
    </ButtonContainer>
  );
});

Button.displayName = "Button";
Button.defaultProps = {
  iconPlacement: "right",
  type: "button",
};
