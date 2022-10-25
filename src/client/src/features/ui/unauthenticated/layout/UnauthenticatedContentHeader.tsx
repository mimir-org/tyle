import { useTheme } from "styled-components";
import { Box, MotionFlexbox } from "../../../../complib/layouts";
import { Heading, Text } from "../../../../complib/text";
import { MotionLogo } from "../../../../content/common/logo/Logo";

export interface UnauthenticatedContentHeaderProps {
  title?: string;
  subtitle?: string;
}

export const UnauthenticatedContentHeader = ({ title, subtitle }: UnauthenticatedContentHeaderProps) => {
  const theme = useTheme();

  return (
    <MotionFlexbox as={"header"} flexDirection={"column"} gap={theme.tyle.spacing.base} layout>
      <MotionLogo layout width={"100px"} height={"50px"} inverse alt="" />
      <Box>
        {title && (
          <Heading as={"h1"} variant={"display-small"}>
            {title}
          </Heading>
        )}
        {subtitle && <Text variant={"headline-small"}>{subtitle}</Text>}
      </Box>
    </MotionFlexbox>
  );
};
