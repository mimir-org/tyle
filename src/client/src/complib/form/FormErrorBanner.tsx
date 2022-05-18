import { useTheme } from "styled-components";
import { ReactNode } from "react";
import { MotionBox } from "../layouts";

interface FormErrorBannerProps {
  children?: ReactNode;
}

/**
 * Banner for displaying global error information in forms
 */
export const FormErrorBanner = ({ children }: FormErrorBannerProps) => {
  const theme = useTheme();

  return (
    <MotionBox
      layout
      {...theme.tyle.animation.fade}
      p={theme.tyle.spacing.small}
      bgColor={theme.tyle.color.error.base}
      color={theme.tyle.color.error.on}
    >
      {children}
    </MotionBox>
  );
};
