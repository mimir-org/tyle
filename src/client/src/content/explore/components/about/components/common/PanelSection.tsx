import { ReactNode } from "react";
import { useTheme } from "styled-components";
import { Flexbox } from "../../../../../../complib/layouts";
import { Heading } from "../../../../../../complib/text";

interface PanelSectionProps {
  title: string;
  children?: ReactNode;
}

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
