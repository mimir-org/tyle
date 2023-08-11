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
      {...theme.mimirorg.animation.fade}
      p={theme.mimirorg.spacing.l}
      bgColor={theme.mimirorg.color.error.base}
      color={theme.mimirorg.color.error.on}
    >
      {children}
    </MotionBox>
  );
};
