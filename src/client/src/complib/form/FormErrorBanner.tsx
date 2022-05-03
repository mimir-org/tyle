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
      {...theme.typeLibrary.animation.fade}
      p={theme.typeLibrary.spacing.small}
      bgColor={theme.typeLibrary.color.error.base}
      color={theme.typeLibrary.color.error.on}
    >
      {children}
    </MotionBox>
  );
};
