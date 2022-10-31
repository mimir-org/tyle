import { VisuallyHidden } from "complib/accessibility";
import { ButtonContainerProps, MotionButtonContainer } from "complib/buttons/Button.styled";
import { Icon } from "complib/media";
import { TextTypes } from "complib/props";
import { Text } from "complib/text";
import { ForwardedRef, forwardRef, isValidElement, ReactElement, ReactNode } from "react";
import { useTheme } from "styled-components";

type ButtonProps = ButtonContainerProps & {
  children: ReactNode;
  icon?: string | ReactElement;
  iconPlacement?: "left" | "right";
  iconOnly?: boolean;
  textVariant?: TextTypes;
};

export const Button = forwardRef((props: ButtonProps, ref: ForwardedRef<HTMLButtonElement>) => {
  const theme = useTheme();
  const { children, icon, iconPlacement, iconOnly, textVariant, ...delegated } = props;
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
        <Text as={"span"} variant={textVariant}>
          {children}
        </Text>
      )}
      {icon && <IconComponent />}
    </MotionButtonContainer>
  );
});

Button.displayName = "Button";
Button.defaultProps = {
  type: "button",
  iconPlacement: "right",
  textVariant: "body-small",
};
