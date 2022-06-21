import styled from "styled-components/macro";

export const ItemDescriptionContainer = styled.button`
  border: 0;
  background-color: transparent;
  text-align: start;

  flex: 1;
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.s};
  color: ${(props) => props.theme.tyle.color.sys.surface.on};

  margin-right: auto;

  width: 400px;
  height: 100px;

  :hover {
    cursor: pointer;
  }
`;
