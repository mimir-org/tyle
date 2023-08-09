import { Flexbox } from "@mimirorg/component-library";
import { Heading } from "complib/text";
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
export const PanelSection = ({ title, children }: PanelSectionProps) => {
  const theme = useTheme();

  return (
    <>
      <Heading as={"h3"} variant={"body-large"} color={theme.tyle.color.sys.surface.on}>
        {title}
      </Heading>
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {children}
      </Flexbox>
    </>
  );
};
