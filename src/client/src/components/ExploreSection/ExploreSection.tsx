import Box from "components/Box";
import Text from "components/Text";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

interface ExploreSectionProps {
  title: string;
  children?: ReactNode;
}

/**
 * A simple layout component for sections inside the Explore component.
 *
 * @param title of the section
 * @param children elements which are wrapped by this layout component
 * @constructor
 */
const ExploreSection = ({ title, children }: ExploreSectionProps) => {
  const theme = useTheme();

  return (
    <Box flex={1} display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.xl} height={"100%"}>
      <Text variant={"headline-large"} color={theme.tyle.color.primary.base}>
        {title}
      </Text>
      {children}
    </Box>
  );
};

export default ExploreSection;
