import { Icon } from "../../../complib/media";
import { Text } from "../../../complib/text";
import { Flexbox } from "../../../complib/layouts";
import { useTheme } from "styled-components";

interface Props {
  name: string;
  icon: string;
}

export const Logo = ({ name, icon }: Props) => {
  const theme = useTheme();

  return (
    <Flexbox alignItems={"center"} gap={theme.typeLibrary.spacing.small}>
      <Icon size={30} src={icon} alt={""} />
      <Text variant={"title-large"}>{name}</Text>
    </Flexbox>
  );
};
