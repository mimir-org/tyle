import styled from "styled-components/macro";

const ItemActionContainer = styled.div`
  position: absolute;
  top: -36px;
  right: 8px;
  display: flex;
  gap: ${(props) => props.theme.mimirorg.spacing.l};
  margin-left: auto;
`;

export default ItemActionContainer;
