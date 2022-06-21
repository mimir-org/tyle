import { useTheme } from "styled-components";
import { Flexbox } from "../../../complib/layouts";
import { Icon } from "../../../complib/media";
import { Text } from "../../../complib/text";

interface Props {
  name: string;
  icon: string;
}

export const Logo = ({ name, icon }: Props) => {
  const theme = useTheme();

  return (
    <Flexbox alignItems={"center"} gap={theme.tyle.spacing.l}>
      <Icon size={30} src={icon} alt={""} />
      <Text variant={"headline-large"}>{name}</Text>
    </Flexbox>
  );
};
