import { PropsWithChildren } from "react";
import { MotionFlexbox } from "../layouts";
import { MotionText, Text } from "../text";
import { ANIMATION, THEME } from "../core";

interface FormFieldProps {
  label?: string;
  error?: { message?: string };
}

/**
 * A component for wrapping form inputs with a label and an error message
 * @param label describing the input
 * @param error message for the given input
 * @param children
 * @constructor
 */
export const FormField = ({ label, error, children }: PropsWithChildren<FormFieldProps>) => (
  <MotionFlexbox layout={"position"} flexDirection={"column"} gap={THEME.SPACING.XS}>
    <Text as={"label"} fontWeight={THEME.FONT.WEIGHTS.BOLD}>
      {label}
    </Text>
    {children}
    {error && error.message && (
      <MotionText color={THEME.COLOR.SEMANTIC.NEGATIVE.BASE} {...ANIMATION.VARIANTS.FADE}>
        {error.message}
      </MotionText>
    )}
  </MotionFlexbox>
);
