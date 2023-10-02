import { Box, MotionFlexbox, Text } from "@mimirorg/component-library";
import { PropsWithChildren } from "react";
import { useTheme } from "styled-components";

interface UsersProps {
  title: string;
}

export const UserList = ({ title, children }: PropsWithChildren<UsersProps>) => {
  const theme = useTheme();

  return (
    <MotionFlexbox flexDirection={"column"} gap={theme.mimirorg.spacing.l} {...theme.mimirorg.animation.fade}>
      <Text variant={"label-large"}>{title}</Text>
      <Box as={"ul"} display={"flex"} flexDirection={"column"} gap={theme.mimirorg.spacing.l} spacing={{ p: "0" }}>
        {children}
      </Box>
    </MotionFlexbox>
  );
};
