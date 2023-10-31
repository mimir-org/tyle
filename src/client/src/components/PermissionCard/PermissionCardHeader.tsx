import { UserCircle } from "@styled-icons/heroicons-outline";
import { Box, Text } from "@mimirorg/component-library";
import { useTheme } from "styled-components";

interface PermissionCardHeaderProps {
  children?: string;
}

export const PermissionCardHeader = ({ children }: PermissionCardHeaderProps) => {
  const theme = useTheme();

  return (
    <Box display={"flex"} gap={theme.mimirorg.spacing.s}>
      <UserCircle size={24} color={theme.mimirorg.color.primary.base} />
      <Text variant={"title-large"}>{children}</Text>
    </Box>
  );
};
