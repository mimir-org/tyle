import styled from "styled-components/macro";

export const AuthenticatedContainer = styled.div`
  height: 100%;
  background-color: ${(props) => props.theme.tyle.color.sys.background.base};
  color: ${(props) => props.theme.tyle.color.sys.background.on};
  overflow: auto;
  --tl-header-height: 56px;
`;

export const AuthenticatedContentContainer = styled.div`
  display: flex;
  height: calc(100% - var(--tl-header-height));
  max-width: 1920px;
  margin: 0 auto;
`;
