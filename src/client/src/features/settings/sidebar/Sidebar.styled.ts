import { getTextRole } from "complib/mixins";
import { Link } from "react-router-dom";
import styled, { css } from "styled-components/macro";

export const SidebarContainer = styled.aside`
  display: flex;
  flex-direction: column;
  width: 250px;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};
`;

interface SidebarLinkProps {
  selected?: boolean;
}

export const SidebarLink = styled(Link)<SidebarLinkProps>`
  color: inherit;
  text-decoration: inherit;

  padding: ${(props) => props.theme.tyle.spacing.base} ${(props) => props.theme.tyle.spacing.l};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};

  ${getTextRole("body-large")};

  ${({ selected, theme }) =>
    selected &&
    css`
      background-color: ${theme.tyle.color.sys.tertiary.container?.base};
      ${getTextRole("title-medium")};
    `}
`;
