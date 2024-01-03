import { Box } from "@mimirorg/component-library";
import { UserCircle } from "@styled-icons/heroicons-outline";
import Text from "components/Text";
import { useTheme } from "styled-components";

interface RoleCardHeaderProps {
  children?: string;
}

const RoleCardHeader = ({ children }: RoleCardHeaderProps) => {
  const theme = useTheme();

  return (
    <Box display={"flex"} gap={theme.tyle.spacing.s}>
      <UserCircle size={24} color={theme.tyle.color.primary.base} />
      <Text variant={"title-large"}>{children}</Text>
    </Box>
  );
};

export default RoleCardHeader;
