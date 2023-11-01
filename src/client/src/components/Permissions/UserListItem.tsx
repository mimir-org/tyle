import { Flexbox, MotionBox, Text } from "@mimirorg/component-library";
import { UserCircle } from "@styled-icons/heroicons-outline";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

export interface UserListItemProps {
  name: string;
  trait: string;
  action?: ReactNode;
}

const UserListItem = ({ name, trait, action }: UserListItemProps) => {
  const theme = useTheme();

  return (
    <MotionBox as={"li"} display={"flex"} flex={1} justifyContent={"space-between"} layout>
      <Flexbox alignItems={"center"} gap={theme.mimirorg.spacing.base}>
        <UserCircle size={18} color={theme.mimirorg.color.primary.base} />
        <Text>{name}</Text>
      </Flexbox>
      <Flexbox alignItems={"center"} gap={theme.mimirorg.spacing.base}>
        <Text variant={"label-large"}>{trait}</Text>
        {action}
      </Flexbox>
    </MotionBox>
  );
};

export default UserListItem;
