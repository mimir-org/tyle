import styled from "styled-components/macro";

export const ItemDescriptionContainer = styled.button`
  border: 0;
  background-color: transparent;
  text-align: start;

  flex: 1;
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.small};
  color: ${(props) => props.theme.tyle.color.surface.on};

  margin-right: auto;

  min-width: 200px;
  max-width: 70ch;

  :hover {
    cursor: pointer;
  }
`;
