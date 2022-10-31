import { UserCircle } from "@styled-icons/heroicons-outline";
import { Button } from "complib/buttons";
import { Popover } from "complib/data-display";
import { Box } from "complib/layouts";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

interface UserMenuProps {
  name: string;
  children?: ReactNode;
}

/**
 * Component that displays a button which shows menu popover
 *
 * @param name
 * @param children
 * @constructor
 */
export const UserMenu = ({ name, children }: UserMenuProps) => {
  const theme = useTheme();

  return (
    <Popover
      align={"end"}
      placement={"bottom"}
      bgColor={theme.tyle.color.sys.background.base}
      color={theme.tyle.color.sys.background.on}
      content={
        <Box display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.xl} width={"170px"}>
          {children}
        </Box>
      }
    >
      <Button
        icon={<UserCircle size={24} />}
        iconPlacement={"left"}
        textVariant={"label-large"}
        mr={`-${theme.tyle.spacing.xl}`}
      >
        {name}
      </Button>
    </Popover>
  );
};
