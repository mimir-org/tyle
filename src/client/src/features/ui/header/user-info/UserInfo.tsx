import { Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { useTheme } from "styled-components";

interface UserInfoProps {
  name: string;
  roles?: string[];
  permissions?: string[];
}

export const UserInfo = ({ name, roles, permissions }: UserInfoProps) => {
  const theme = useTheme();

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.s}>
      <Text variant={"title-medium"}>{name}</Text>
      {roles && (
        <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xs}>
          <Text variant={"title-small"}>{"Roles:"}</Text>
          {roles?.map((x, i) => (
            <Text key={x + i} variant={"label-medium"}>
              {x}
            </Text>
          ))}
        </Flexbox>
      )}
      {permissions && (
        <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xs}>
          <Text variant={"title-small"}>{"Permissions:"}</Text>
          {permissions?.map((x, i) => (
            <Text key={i} variant={"label-small"}>
              {x}
            </Text>
          ))}
        </Flexbox>
      )}
    </Flexbox>
  );
};
