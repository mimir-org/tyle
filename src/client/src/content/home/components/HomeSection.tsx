import { ReactNode } from "react";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { Text } from "../../../complib/text";

interface HomeSectionProps {
  title: string;
  children?: ReactNode;
}

/**
 * A simple layout component for sections inside the Home component.
 *
 * @param title of the section
 * @param children elements which are wrapped by this layout component
 * @constructor
 */
export const HomeSection = ({ title, children }: HomeSectionProps) => {
  const theme = useTheme();

  return (
    <Box flex={1} display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.xl} height={"100%"}>
      <Text variant={"headline-large"} color={theme.tyle.color.sys.primary.base}>
        {title}
      </Text>
      {children}
    </Box>
  );
};
