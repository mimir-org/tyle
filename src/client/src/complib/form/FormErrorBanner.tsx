import { ReactNode } from "react";
import { MotionBox } from "../layouts";
import { ANIMATION, THEME } from "../core";

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
    p={THEME.SPACING.SMALL}
    border={`2px solid ${THEME.COLOR.SEMANTIC.NEGATIVE.DARK}`}
    bgColor={THEME.COLOR.SEMANTIC.NEGATIVE.BASE}
    color={THEME.COLOR.TEXT.PRIMARY.INVERTED}
  >
    {children}
  </MotionBox>
);
