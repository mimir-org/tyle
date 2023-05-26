import { ArrowSmallRight } from "@styled-icons/heroicons-outline";
import { Link } from "common/types/link";
import { Button } from "complib/buttons";
import { Popover } from "complib/data-display";
import { Box } from "complib/layouts";
import { PlainLink } from "features/common/plain-link";
import { useTheme } from "styled-components";

interface LinkMenuProps {
  name: string;
  links: Link[];
  justifyContent?: "space-between" | "space-around" | "center" | "start" | "end" | "normal";
}

/**
 * Component which displays a button that has a popover with links
 *
 * @param name text on menu button
 * @param links shortcuts presented in popover
 * @param justifyContent how to arrange the button text and icons in the list
 * @constructor
 */
export const LinkMenu = ({ name, links, justifyContent }: LinkMenuProps) => {
  const theme = useTheme();

  return (
    <Popover
      align={"end"}
      placement={"bottom"}
      bgColor={theme.tyle.color.sys.background.base}
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
      <Button flexShrink={"0"}>{name}</Button>
    </Popover>
  );
};
