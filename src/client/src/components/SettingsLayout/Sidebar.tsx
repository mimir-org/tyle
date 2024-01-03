import { useGetAttributesByState } from "api/attribute.queries";
import { useGetBlocksByState } from "api/block.queries";
import { useGetTerminalsByState } from "api/terminal.queries";
import Divider from "components/Divider";
import Flexbox from "components/Flexbox";
import Heading from "components/Heading";
import { useGetAllUsersMapped } from "hooks/useGetAllUsersMapped";
import { Fragment } from "react";
import { useLocation } from "react-router-dom";
import { useTheme } from "styled-components";
import { State } from "types/common/state";
import { LinkGroup } from "types/linkGroup";
import { SidebarContainer, SidebarLink } from "./Sidebar.styled";

interface SidebarProps {
  title: string;
  groups: LinkGroup[];
}

const Sidebar = ({ title, groups }: SidebarProps) => {
  const theme = useTheme();
  const location = useLocation();

  const attributesInReview = useGetAttributesByState(State.Review);
  const terminalsInReview = useGetTerminalsByState(State.Review);
  const blocksInReview = useGetBlocksByState(State.Review);
  const pendingUsers = useGetAllUsersMapped().filter((e) => e.roles.length === 0);
  const reviewData = [attributesInReview, terminalsInReview, blocksInReview];
  let numberOfTypesInReview = 0;

  for (const query of reviewData) {
    if (query.data) {
      numberOfTypesInReview += query.data.length;
    }
  }

  const linkText = (name: string) => {
    switch (name) {
      case "Approval": {
        return name + (numberOfTypesInReview ? ` (${numberOfTypesInReview})` : "");
      }
      case "Access": {
        return name + (pendingUsers.length ? ` (${pendingUsers.length})` : "");
      }
      default:
        return name;
    }
  };

  return (
    <SidebarContainer>
      <Heading variant={"headline-large"}>{title}</Heading>
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.base}>
        {groups.map((group, i) => (
          <Fragment key={i}>
            {group.links.map((link, i) => (
              <SidebarLink key={`${i},${link.name}`} to={link.path} selected={location.pathname.includes(link.path)}>
                {linkText(link.name)}
              </SidebarLink>
            ))}
            <Divider color={theme.tyle.color.outline.base} />
          </Fragment>
        ))}
      </Flexbox>
    </SidebarContainer>
  );
};

export default Sidebar;
