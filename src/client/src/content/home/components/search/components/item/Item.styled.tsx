import styled from "styled-components/macro";

export const ItemActionContainer = styled.div`
  position: absolute;
  top: -36px;
  right: 8px;
  display: flex;
  gap: ${(props) => props.theme.tyle.spacing.l};
  margin-left: auto;
`;
