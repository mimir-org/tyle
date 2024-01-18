import styled from "styled-components/macro";

const HeaderContainer = styled.header`
  display: flex;
  align-items: center;
  justify-content: space-between;
  height: var(--tl-header-height);
  padding: ${(props) => props.theme.tyle.spacing.base} ${(props) => props.theme.tyle.spacing.xxxl};
  background-color: ${(props) => props.theme.tyle.color.primary.base};
  color: ${(props) => props.theme.tyle.color.primary.on};
  box-shadow: ${(props) => props.theme.tyle.shadow.small};
`;

export default HeaderContainer;
