import { MotionBox } from "complib/layouts";
import { Heading } from "complib/text";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

interface FormSectionProps {
  title: string;
  action?: ReactNode;
  children?: ReactNode;
  error?: { message?: string };
}

/**
 * A simple layout component for sections inside the settings area.
 *
 * @param title of the section
 * @param children elements which are wrapped by this layout component
 * @constructor
 */
export const SettingsSection = ({ title, children }: FormSectionProps) => {
  const theme = useTheme();

  return (
    <MotionBox as={"section"} {...theme.tyle.animation.fade}>
      <Heading as={"h2"} variant={"headline-medium"}>
        {title}
      </Heading>
      {children}
    </MotionBox>
  );
};
