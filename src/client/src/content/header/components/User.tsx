import { Text } from "../../../complib/text";

interface Props {
  name: string;
}

export const User = ({ name }: Props) => <Text variant={"title-medium"}>{name}</Text>;
