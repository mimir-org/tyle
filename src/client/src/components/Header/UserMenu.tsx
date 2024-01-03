import { Button, Popover } from "@mimirorg/component-library";
import { UserCircle } from "@styled-icons/heroicons-outline";
import Box from "components/Box";
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
const UserMenu = ({ name, children }: UserMenuProps) => {
  const theme = useTheme();

  return (
    <Popover
      align={"end"}
      placement={"bottom"}
      bgColor={theme.tyle.color.background.base}
      color={theme.tyle.color.background.on}
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
        spacing={{ mr: `-${theme.tyle.spacing.xl}` }}
      >
        {name}
      </Button>
    </Popover>
  );
};

export default UserMenu;
