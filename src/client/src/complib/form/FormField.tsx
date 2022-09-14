import { PropsWithChildren } from "react";
import { useTheme } from "styled-components";
import { Box, Flexbox, MotionFlexbox } from "../layouts";
import { MotionText, Text } from "../text";
import { ConditionalWrapper } from "../utils";

interface FormFieldProps {
  label?: string;
  error?: { message?: string };
}

/**
 * A component for wrapping form inputs with an error message and/or a label
 *
 * @param label describing the input
 * @param error message for the given input
 * @param children
 * @constructor
 */
export const FormField = ({ label, error, children }: PropsWithChildren<FormFieldProps>) => {
  const theme = useTheme();
  const hasLabel = !!label?.length;

  return (
    <MotionFlexbox layout flexDirection={"column"} gap={theme.tyle.spacing.s}>
      <ConditionalWrapper
        condition={hasLabel}
        wrapper={(c) => (
          <Flexbox as={"label"} flexDirection={"column"} gap={theme.tyle.spacing.xs}>
            <Box borderLeft={"1px solid transparent"}>
              <Text
                as={"span"}
                variant={"label-large"}
                color={theme.tyle.color.sys.surface.variant.on}
                pl={theme.tyle.spacing.l}
              >
                {label}
              </Text>
            </Box>
            {c}
          </Flexbox>
        )}
      >
        <>{children}</>
      </ConditionalWrapper>

      {error && error.message && (
        <MotionText variant={"label-medium"} color={theme.tyle.color.sys.error.base} {...theme.tyle.animation.fade}>
          {error.message}
        </MotionText>
      )}
    </MotionFlexbox>
  );
};
