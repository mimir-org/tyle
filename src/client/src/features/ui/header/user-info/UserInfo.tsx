import { Flexbox, Text } from "@mimirorg/component-library";
import { useTheme } from "styled-components";

interface UserInfoProps {
  name: string;
  roles?: string[];
  permissions?: string[];
}

export const UserInfo = ({ name, roles, permissions }: UserInfoProps) => {
  const theme = useTheme();

  return (
    <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.s}>
      <Text variant={"title-medium"}>{name}</Text>
      {roles && roles.length > 0 && (
        <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.xs}>
          {roles?.map((x, i) => (
            <Text style={{ color: "gray" }} key={x + i} variant={"label-medium"}>
              {x}
            </Text>
          ))}
        </Flexbox>
      )}
      {permissions && permissions.length > 0 && (
        <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.xs}>
          {permissions?.map((x, i) => (
            <Text style={{ color: "gray" }} key={i} variant={"label-small"}>
              {x}
            </Text>
          ))}
        </Flexbox>
      )}
    </Flexbox>
  );
};
