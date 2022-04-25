import { ReactNode } from "react";
import { MotionBox } from "../layouts";
import { ANIMATION, theme } from "../core";

interface FormErrorBannerProps {
  children?: ReactNode;
}

/**
 * Banner for displaying global error information in forms
 */
export const FormErrorBanner = ({ children }: FormErrorBannerProps) => (
  <MotionBox
    layout
    {...ANIMATION.VARIANTS.FADE}
    p={theme.spacing.small}
    bgColor={theme.color.error.base}
    color={theme.color.error.on}
  >
    {children}
  </MotionBox>
);
