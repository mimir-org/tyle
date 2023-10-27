import { LinkGroup } from "common/types/linkGroup";
import { Divider, Flexbox, Heading } from "@mimirorg/component-library";
import { useGetPendingUsers } from "external/sources/company/company.queries";
import { SidebarContainer, SidebarLink } from "features/settings/layout/sidebar/Sidebar.styled";
import { Fragment } from "react";
import { useTranslation } from "react-i18next";
import { useLocation } from "react-router-dom";
import { useTheme } from "styled-components";
import { useGetAttributesByState } from "external/sources/attribute/attribute.queries";
import { State } from "common/types/common/state";
import { useGetTerminalsByState } from "external/sources/terminal/terminal.queries";
import { useGetBlocksByState } from "external/sources/block/block.queries";

interface SidebarProps {
  title: string;
  groups: LinkGroup[];
}

export const Sidebar = ({ title, groups }: SidebarProps) => {
  const { t } = useTranslation("settings");
  const theme = useTheme();
  const location = useLocation();

  const attributesInReview = useGetAttributesByState(State.Review);
  const terminalsInReview = useGetTerminalsByState(State.Review);
  const blocksInReview = useGetBlocksByState(State.Review);

  const reviewData = [attributesInReview, terminalsInReview, blocksInReview];
  let numberOfTypesInReview = 0;
  
  for (const query of reviewData) {
    if (query.data) {
      numberOfTypesInReview += query.data.length;
    }
  }

  const pendingUsers = useGetPendingUsers();

  const linkText = (name: string) => {
    switch (name) {
      case t("approval.title"): {
        return name + (numberOfTypesInReview ? ` (${numberOfTypesInReview})` : "");
      }
      case t("access.title"): {
        return name + (pendingUsers.data?.length ? ` (${pendingUsers.data.length})` : "");
      }
      default:
        return name;
    }
  };

  return (
    <SidebarContainer>
      <Heading variant={"headline-large"}>{title}</Heading>
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.base}>
        {groups.map((group, i) => (
          <Fragment key={i}>
            {group.links.map((link, i) => (
              <SidebarLink key={`${i},${link.name}`} to={link.path} selected={location.pathname.includes(link.path)}>
                {linkText(link.name)}
              </SidebarLink>
            ))}
            <Divider color={theme.mimirorg.color.outline.base} />
          </Fragment>
        ))}
      </Flexbox>
    </SidebarContainer>
  );
};
