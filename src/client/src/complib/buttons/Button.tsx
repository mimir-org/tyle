import { ForwardedRef, forwardRef, isValidElement, ReactElement, ReactNode } from "react";
import { useTheme } from "styled-components";
import { VisuallyHidden } from "../accessibility";
import { Icon } from "../media";
import { Text } from "../text";
import { ButtonContainerProps, MotionButtonContainer } from "./Button.styled";

type ButtonProps = ButtonContainerProps & {
  children: ReactNode;
  icon?: string | ReactElement;
  iconPlacement?: "left" | "right";
  iconOnly?: boolean;
};

export const Button = forwardRef((props: ButtonProps, ref: ForwardedRef<HTMLButtonElement>) => {
  const theme = useTheme();
  const { children, icon, iconPlacement, iconOnly, ...delegated } = props;
  const IconComponent = () => (isValidElement(icon) ? icon : <Icon src={icon} alt="" />);

  return (
    <MotionButtonContainer
      ref={ref}
      iconOnly={iconOnly}
      iconPlacement={iconPlacement}
      {...theme.tyle.animation.buttonTap}
      {...delegated}
    >
      {icon && iconOnly ? (
        <VisuallyHidden>{children}</VisuallyHidden>
      ) : (
        <Text as={"span"} variant={"body-small"}>
          {children}
        </Text>
      )}
      {icon && <IconComponent />}
    </MotionButtonContainer>
  );
});

Button.displayName = "Button";
Button.defaultProps = {
  iconPlacement: "right",
  type: "button",
};
