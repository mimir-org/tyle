import { ExclamationCircle } from "@styled-icons/heroicons-outline";
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
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.s}>
      <MotionFlexbox layout as={hasLabel ? "label" : "div"} flexDirection={"column"} gap={theme.tyle.spacing.xs}>
        <ConditionalWrapper
          condition={hasLabel}
          wrapper={(c) => (
            <>
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
            </>
          )}
        >
          <>{children}</>
        </ConditionalWrapper>
      </MotionFlexbox>

      {error && error.message && (
        <Flexbox alignItems={"center"} gap={theme.tyle.spacing.s}>
          <ExclamationCircle
            size={theme.tyle.typography.sys.roles.label.medium.size}
            color={theme.tyle.color.sys.error.base}
          />
          <MotionText variant={"label-medium"} color={theme.tyle.color.sys.error.base} {...theme.tyle.animation.fade}>
            {error.message}
          </MotionText>
        </Flexbox>
      )}
    </Flexbox>
  );
};
