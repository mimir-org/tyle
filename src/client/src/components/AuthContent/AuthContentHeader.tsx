import { Box, Heading, MotionFlexbox, Text } from "@mimirorg/component-library";
import { MotionLogo } from "components/Logo/Logo";
import { useTheme } from "styled-components";

export interface AuthContentHeaderProps {
  title?: string;
  subtitle?: string;
}

const AuthContentHeader = ({ title, subtitle }: AuthContentHeaderProps) => {
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

export default AuthContentHeader;
