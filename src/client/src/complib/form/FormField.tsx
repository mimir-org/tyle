import { ExclamationCircle } from "@styled-icons/heroicons-outline";
import { FormFieldLabelText } from "complib/form/FormField.styled";
import { Flexbox, MotionFlexbox } from "complib/layouts";
import { Text } from "complib/text";
import { ConditionalWrapper } from "complib/utils";
import { PropsWithChildren } from "react";
import { useTheme } from "styled-components";

interface FormFieldProps {
  label?: string;
  error?: { message?: string };
  indent?: boolean;
}

/**
 * A component for wrapping form inputs with an error message and/or a label
 *
 * @param label describing the input
 * @param error message for the given input
 * @param indent if the label should be indented
 * @param children
 * @constructor
 */
export const FormField = ({ label, error, indent = true, children }: PropsWithChildren<FormFieldProps>) => {
  const theme = useTheme();
  const hasLabel = !!label?.length;

  return (
    <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.s}>
      <MotionFlexbox
        layout={"preserve-aspect"}
        as={hasLabel ? "label" : "div"}
        flexDirection={"column"}
        gap={theme.mimirorg.spacing.xs}
      >
        <ConditionalWrapper
          condition={hasLabel}
          wrapper={(c) => (
            <>
              <FormFieldLabelText indent={indent}>{label}</FormFieldLabelText>
              {c}
            </>
          )}
        >
          <>{children}</>
        </ConditionalWrapper>
      </MotionFlexbox>

      {error && error.message && (
        <MotionFlexbox layout alignItems={"center"} gap={theme.mimirorg.spacing.s} {...theme.mimirorg.animation.fade}>
          <ExclamationCircle
            size={theme.mimirorg.typography.roles.label.medium.size}
            color={theme.mimirorg.color.error.base}
          />
          <Text variant={"label-medium"} color={theme.mimirorg.color.error.base}>
            {error.message}
          </Text>
        </MotionFlexbox>
      )}
    </Flexbox>
  );
};
