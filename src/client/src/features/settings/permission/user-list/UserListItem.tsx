import { UserCircle } from "@styled-icons/heroicons-outline";
import { Flexbox, MotionBox, Text } from "@mimirorg/component-library";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

export interface UserListItemProps {
  name: string;
  trait: string;
  action?: ReactNode;
}

export const UserListItem = ({ name, trait, action }: UserListItemProps) => {
  const theme = useTheme();

  return (
    <MotionBox as={"li"} display={"flex"} flex={1} justifyContent={"space-between"} layout>
      <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
        <UserCircle size={18} color={theme.tyle.color.sys.primary.base} />
        <Text>{name}</Text>
      </Flexbox>
      <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
        <Text variant={"label-large"}>{trait}</Text>
        {action}
      </Flexbox>
    </MotionBox>
  );
};
