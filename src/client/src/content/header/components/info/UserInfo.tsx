import { useTheme } from "styled-components";
import { Flexbox } from "../../../../complib/layouts";
import { Text } from "../../../../complib/text";

interface UserInfoProps {
  name: string;
  permissions: string[];
}

export const UserInfo = ({ name, permissions }: UserInfoProps) => {
  const theme = useTheme();

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.s}>
      <Text variant={"title-medium"}>{name}</Text>
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xs}>
        {permissions?.map((x, i) => (
          <Text key={i} variant={"label-small"}>
            {x}
          </Text>
        ))}
      </Flexbox>
    </Flexbox>
  );
};
