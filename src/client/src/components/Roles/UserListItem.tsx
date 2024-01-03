import { MotionBox, Text } from "@mimirorg/component-library";
import { UserCircle } from "@styled-icons/heroicons-outline";
import Flexbox from "components/Flexbox";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

export interface UserListItemProps {
  name: string;
  role: string[];
  action?: ReactNode;
}

const UserListItem = ({ name, role, action }: UserListItemProps) => {
  const theme = useTheme();

  return (
    <MotionBox as={"li"} display={"flex"} flex={1} justifyContent={"space-between"} layout>
      <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
        <UserCircle size={18} color={theme.tyle.color.primary.base} />
        <Text>{name}</Text>
      </Flexbox>
      <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
        <Text variant={"label-large"}>{role}</Text>
        {action}
      </Flexbox>
    </MotionBox>
  );
};

export default UserListItem;
