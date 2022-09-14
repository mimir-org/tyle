import { ReactNode } from "react";
import { useTheme } from "styled-components";
import { Box, Flexbox } from "../../../../complib/layouts";
import { MotionText, Text } from "../../../../complib/text";

interface FormSectionProps {
  title: string;
  action?: ReactNode;
  children?: ReactNode;
  error?: { message?: string };
}

/**
 * A simple layout component for sections inside the entity forms.
 *
 * @param title of the section
 * @param action element which manipulates some state of the section (e.g. add attribute button, add terminal button etc.)
 * @param children elements which are wrapped by this layout component
 * @param error error message for section that appears below title and action button
 * @constructor
 */
export const FormSection = ({ title, action, children, error }: FormSectionProps) => {
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
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.s}>
        <Box display={"flex"} gap={theme.tyle.spacing.xl} alignItems={"center"}>
          <Text variant={"title-large"}>{title}</Text>
          {action}
        </Box>

        {error && error.message && (
          <MotionText variant={"label-large"} color={theme.tyle.color.sys.error.base} {...theme.tyle.animation.fade}>
            {error.message}
          </MotionText>
        )}
      </Flexbox>
      {children}
    </Box>
  );
};
