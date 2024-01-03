import { Box, MotionBox, MotionText, Text } from "@mimirorg/component-library";
import Flexbox from "components/Flexbox";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

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
const FormSection = ({ title, action, children, error }: FormSectionProps) => {
  const theme = useTheme();

  return (
    <MotionBox
      layout={"position"}
      as={"fieldset"}
      display={"flex"}
      flexDirection={"column"}
      justifyContent={"center"}
      gap={theme.tyle.spacing.xxxl}
      border={0}
      p={"0"}
      {...theme.tyle.animation.fade}
    >
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.s}>
        <Box display={"flex"} gap={theme.tyle.spacing.xl} alignItems={"center"}>
          <Text variant={"title-large"}>{title}</Text>
          {action}
        </Box>

        {error && error.message && (
          <MotionText variant={"label-large"} color={theme.tyle.color.error.base} {...theme.tyle.animation.fade}>
            {error.message}
          </MotionText>
        )}
      </Flexbox>
      {children}
    </MotionBox>
  );
};

export default FormSection;
