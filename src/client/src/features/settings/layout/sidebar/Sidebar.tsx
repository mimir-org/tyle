import { LinkGroup } from "common/types/linkGroup";
import { Divider } from "complib/data-display";
import { Flexbox } from "complib/layouts";
import { Heading } from "complib/text";
import { SidebarContainer, SidebarLink } from "features/settings/layout/sidebar/Sidebar.styled";
import { Fragment } from "react";
import { useLocation } from "react-router-dom";
import { useTheme } from "styled-components";

interface SidebarProps {
  title: string;
  groups: LinkGroup[];
}

export const Sidebar = ({ title, groups }: SidebarProps) => {
  const theme = useTheme();
  const location = useLocation();

  return (
    <SidebarContainer>
      <Heading variant={"headline-large"}>{title}</Heading>
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.base}>
        {groups.map((group, i) => (
          <Fragment key={i}>
            {group.links.map((link) => (
              <SidebarLink key={link.name} to={link.path} selected={location.pathname.includes(link.path)}>
                {link.name}
              </SidebarLink>
            ))}
            <Divider color={theme.tyle.color.sys.outline.base} />
          </Fragment>
        ))}
      </Flexbox>
    </SidebarContainer>
  );
};
