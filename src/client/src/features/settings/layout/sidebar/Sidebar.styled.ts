import { getTextRole } from "complib/mixins";
import { Link } from "react-router-dom";
import styled, { css } from "styled-components/macro";

export const SidebarContainer = styled.aside`
  flex: 1;
  display: flex;
  flex-direction: column;
  max-width: 350px;
  min-width: 200px;
  gap: ${(props) => props.theme.mimirorg.spacing.xxxl};
`;

interface SidebarLinkProps {
  selected?: boolean;
}

export const SidebarLink = styled(Link)<SidebarLinkProps>`
  color: inherit;
  text-decoration: inherit;

  padding: ${(props) => props.theme.mimirorg.spacing.base} ${(props) => props.theme.mimirorg.spacing.l};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.medium};

  ${getTextRole("body-large")};

  ${({ selected, theme }) =>
    selected &&
    css`
      background-color: ${theme.mimirorg.color.tertiary.container?.base};
      ${getTextRole("title-medium")};
    `}
`;
