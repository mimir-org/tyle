import { Box, MotionFlexbox } from "complib/layouts";
import { Text } from "complib/text";
import { PropsWithChildren } from "react";
import { useTheme } from "styled-components";

interface UsersProps {
  title: string;
}

export const UserList = ({ title, children }: PropsWithChildren<UsersProps>) => {
  const theme = useTheme();

  return (
    <MotionFlexbox flexDirection={"column"} gap={theme.tyle.spacing.l} {...theme.tyle.animation.fade}>
      <Text variant={"label-large"}>{title}</Text>
      <Box as={"ul"} display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.l} p={"0"}>
        {children}
      </Box>
    </MotionFlexbox>
  );
};
