import { MotionBox } from "complib/layouts";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

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
      p={theme.tyle.spacing.l}
      bgColor={theme.tyle.color.sys.error.base}
      color={theme.tyle.color.sys.error.on}
    >
      {children}
    </MotionBox>
  );
};
