import { UserCircle } from "@styled-icons/heroicons-outline";
import { useTheme } from "styled-components";
import { Flexbox } from "../../../complib/layouts";
import { Text } from "../../../complib/text";

interface Props {
  name: string;
}

export const User = ({ name }: Props) => {
  const theme = useTheme();

  return (
    <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
      <UserCircle size={24} />
      <Text variant={"body-medium"}>{name}</Text>
    </Flexbox>
  );
};
