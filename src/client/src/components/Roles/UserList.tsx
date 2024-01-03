import { Box } from "@mimirorg/component-library";
import { MotionFlexbox } from "components/Flexbox";
import Text from "components/Text";
import { PropsWithChildren } from "react";
import { useTheme } from "styled-components";

interface UsersProps {
  title: string;
}

const UserList = ({ title, children }: PropsWithChildren<UsersProps>) => {
  const theme = useTheme();

  return (
    <MotionFlexbox flexDirection={"column"} gap={theme.tyle.spacing.l} {...theme.tyle.animation.fade}>
      <Text variant={"label-large"}>{title}</Text>
      <Box as={"ul"} display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.l} spacing={{ p: "0" }}>
        {children}
      </Box>
    </MotionFlexbox>
  );
};

export default UserList;
