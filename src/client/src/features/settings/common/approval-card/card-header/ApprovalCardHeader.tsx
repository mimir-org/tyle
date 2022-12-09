import { Share } from "@styled-icons/heroicons-outline";
import { Box } from "complib/layouts";
import { Text } from "complib/text";
import { useTheme } from "styled-components";

interface ApprovalCardHeaderProps {
  children: string;
}

export const ApprovalCardHeader = ({ children }: ApprovalCardHeaderProps) => {
  const theme = useTheme();

  return (
    <Box display={"flex"} gap={theme.tyle.spacing.s}>
      <Share size={24} color={theme.tyle.color.sys.primary.base} />
      <Text variant={"title-large"}>{children}</Text>
    </Box>
  );
};
