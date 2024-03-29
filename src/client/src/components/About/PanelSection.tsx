import Flexbox from "components/Flexbox";
import Heading from "components/Heading";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

interface PanelSectionProps {
  title: string;
  children?: ReactNode;
}

/**
 * @param title the title of the section
 * @param children the children of the section
 * @constructor
 * @example
 * <PanelSection title={"My Section"}>
 *   <MyComponent />
 * </PanelSection>
 */
const PanelSection = ({ title, children }: PanelSectionProps) => {
  const theme = useTheme();

  return (
    <>
      <Heading as={"h6"} color={theme.tyle.color.surface.on}>
        {title}
      </Heading>
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {children}
      </Flexbox>
    </>
  );
};

export default PanelSection;
