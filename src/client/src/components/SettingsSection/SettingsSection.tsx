import { Heading, MotionBox } from "@mimirorg/component-library";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

interface SettingsSectionProps {
  title: string;
  children?: ReactNode;
}

/**
 * A simple layout component for sections inside the settings area.
 *
 * @param title of the section
 * @param children elements which are wrapped by this layout component
 * @constructor
 */
const SettingsSection = ({ title, children }: SettingsSectionProps) => {
  const theme = useTheme();

  return (
    <MotionBox as={"section"} {...theme.mimirorg.animation.fade}>
      <Heading as={"h2"} variant={"headline-medium"} spacing={{ mb: theme.mimirorg.spacing.xxxl }}>
        {title}
      </Heading>
      {children}
    </MotionBox>
  );
};

export default SettingsSection;