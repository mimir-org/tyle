import { ReactNode } from "react";
import { useTheme } from "styled-components";
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
      bgColor={theme.tyle.color.sys.error.base}
      color={theme.tyle.color.sys.error.on}
    >
      {children}
    </MotionBox>
  );
};
