import { ReactNode } from "react";
import { useTheme } from "styled-components";
import { Box, Flexbox } from "../../../../complib/layouts";
import { Text } from "../../../../complib/text";

interface FormSectionProps {
  title: string;
  action?: ReactNode;
  children?: ReactNode;
}

/**
 * A simple layout component for sections inside the entity forms.
 *
 * @param title of the section
 * @param action element which manipulates some state of the section (e.g. add attribute button, add terminal button etc.)
 * @param children elements which are wrapped by this layout component
 * @constructor
 */
export const FormSection = ({ title, action, children }: FormSectionProps) => {
  const theme = useTheme();

  return (
    <Box
      as={"fieldset"}
      display={"flex"}
      flexDirection={"column"}
      justifyContent={"center"}
      gap={theme.tyle.spacing.xxxl}
      border={0}
      p={"0"}
    >
      <Flexbox gap={theme.tyle.spacing.xl} alignItems={"center"}>
        <Text variant={"title-large"}>{title}</Text>
        {action}
      </Flexbox>
      {children}
    </Box>
  );
};
