import { ExclamationCircle } from "@styled-icons/heroicons-outline";
import ConditionalWrapper from "components/ConditionalWrapper";
import Flexbox from "components/Flexbox";
import Text from "components/Text";
import { PropsWithChildren } from "react";
import { useTheme } from "styled-components";
import { FormFieldLabelText } from "./FormField.styled";

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
const FormField = ({ label, error, indent = true, children }: PropsWithChildren<FormFieldProps>) => {
  const theme = useTheme();
  const hasLabel = !!label?.length;

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.s}>
      <Flexbox as={hasLabel ? "label" : "div"} flexDirection={"column"} gap={theme.tyle.spacing.xs}>
        <ConditionalWrapper
          condition={hasLabel}
          wrapper={(c) => (
            <>
              <FormFieldLabelText indent={indent} variant={"label-medium"}>
                {label}
              </FormFieldLabelText>
              {c}
            </>
          )}
        >
          <>{children}</>
        </ConditionalWrapper>
      </Flexbox>

      {error && error.message && (
        <Flexbox alignItems={"center"} gap={theme.tyle.spacing.s} {...theme.tyle.animation.fade}>
          <ExclamationCircle size={"14px"} color={theme.tyle.color.error.base} />
          <Text variant={"label-medium"} color={theme.tyle.color.error.base}>
            {error.message}
          </Text>
        </Flexbox>
      )}
    </Flexbox>
  );
};

export default FormField;
