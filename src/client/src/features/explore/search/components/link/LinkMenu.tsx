import { ArrowSmRight } from "@styled-icons/heroicons-outline";
import { PlainLink } from "common/components/plain-link";
import { Button } from "complib/buttons";
import { Popover } from "complib/data-display";
import { Box } from "complib/layouts";
import { Link } from "features/explore/search/types/link";
import { useTheme } from "styled-components";

interface LinkMenuProps {
  name: string;
  links: Link[];
}

/**
 * Component which displays a button that has a popover with links
 *
 * @param name text on menu button
 * @param links shortcuts presented in popover
 * @constructor
 */
export const LinkMenu = ({ name, links }: LinkMenuProps) => {
  const theme = useTheme();

  return (
    <Popover
      align={"end"}
      placement={"bottom"}
      bgColor={theme.tyle.color.sys.background.base}
      content={
        <Box display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.base} minWidth={"170px"}>
          {links.map((link, index) => (
            <PlainLink key={index} tabIndex={-1} to={link.path}>
              <Button
                tabIndex={0}
                as={"span"}
                variant={"text"}
                textVariant={"label-large"}
                icon={<ArrowSmRight size={24} />}
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
