import { Box, Button, Popover } from "@mimirorg/component-library";
import { ArrowSmallRight } from "@styled-icons/heroicons-outline";
import PlainLink from "components/PlainLink";
import { useTheme } from "styled-components";
import { Link } from "types/link";

interface LinkMenuProps {
  name: string;
  links: Link[];
  justifyContent?: "space-between" | "space-around" | "center" | "start" | "end" | "normal";
  disabled?: boolean;
}

/**
 * Component which displays a button that has a popover with links
 *
 * @param name text on menu button
 * @param links shortcuts presented in popover
 * @param disabled whether the button is disabled
 * @param justifyContent how to arrange the button text and icons in the list
 * @constructor
 */
const LinkMenu = ({ name, links, justifyContent, disabled }: LinkMenuProps) => {
  const theme = useTheme();

  return (
    <Popover
      align={"end"}
      placement={"bottom"}
      bgColor={theme.tyle.color.background.base}
      content={
        <Box display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.base} minWidth={"170px"}>
          {links.map((link, index) => (
            <PlainLink key={index + link.path} tabIndex={-1} to={link.path}>
              <Button
                tabIndex={0}
                as={"span"}
                variant={"text"}
                textVariant={"label-large"}
                justifyContent={justifyContent ?? "normal"}
                icon={<ArrowSmallRight size={24} />}
                width={"100%"}
              >
                {link.name}
              </Button>
            </PlainLink>
          ))}
        </Box>
      }
    >
      <Button disabled={disabled} flexShrink={"0"}>
        {name}
      </Button>
    </Popover>
  );
};

export default LinkMenu;
