import { Box, MotionFlexbox } from "@mimirorg/component-library";
import { Heading, Text } from "complib/text";
import { MotionLogo } from "features/common/logo/Logo";
import { useTheme } from "styled-components";

export interface AuthContentHeaderProps {
  title?: string;
  subtitle?: string;
}

export const AuthContentHeader = ({ title, subtitle }: AuthContentHeaderProps) => {
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
