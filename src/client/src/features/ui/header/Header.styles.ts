import styled from "styled-components/macro";

export const HeaderContainer = styled.header`
  display: flex;
  align-items: center;
  justify-content: space-between;
  height: var(--tl-header-height);
  padding: ${(props) => props.theme.mimirorg.spacing.base} ${(props) => props.theme.mimirorg.spacing.xxxl};
  background-color: ${(props) => props.theme.mimirorg.color.primary.base};
  color: ${(props) => props.theme.mimirorg.color.primary.on};
  box-shadow: ${(props) => props.theme.mimirorg.shadow.small};
`;
